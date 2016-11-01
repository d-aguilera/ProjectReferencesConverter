//------------------------------------------------------------------------------
// <copyright file="ProjectReferencesConverter.cs" company="Aurea Software">
//     Copyright (c) Aurea Software.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using EnvDTE;
using EnvDTE80;

using Microsoft.VisualStudio.Shell;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using VSLangProj;

namespace ProjectReferencesConverter
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ProjectReferencesConverter
    {
        #region Auto-generated code

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("8731df0d-bec0-43dd-a002-de3e0ac6463e");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectReferencesConverter"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ProjectReferencesConverter(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            var commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ProjectReferencesConverter Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ProjectReferencesConverter(package);
        }

        #endregion

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var dte = (_DTE)ServiceProvider.GetService(typeof(DTE));

            var projectsRootPath = GetProjectsRootPath(Path.GetDirectoryName(dte.Solution.FullName));
            if (string.IsNullOrEmpty(projectsRootPath))
                return;

            var projFilters = GetProjectFilters();
            if (null == projFilters)
                return;

            // convert filters (wildcards) into regex patterns
            var regexFilters = projFilters
                .Select(pattern => Regex.Replace(pattern, @"\.", m => @"\" + m.Value))
                .Select(pattern => Regex.Replace(pattern, @"\*|\?", m => "." + m.Value))
                ;

            var added = 0;
            var converted = 0;
            var remapped = 0;
            var warnings = new List<string>();

            try
            {
                dte.StatusBar.Animate(true, vsStatusAnimation.vsStatusAnimationFind);
                UpdateStatus(dte, "Searching for project files...");

                var task1 = System.Threading.Tasks.Task.Run(() =>
                    FindProjectFiles(projectsRootPath, projFilters)
                );

                System.Threading.Tasks.Task.WaitAll(task1);

                var projectFilesFound = task1.Result;

                UpdateStatus(dte, $"Found {projectFilesFound.Count} projects. Analyzing solution...");

                var projectsInSolution = new Dictionary<string, VSProject>();
                var projectsToAnalyze = new Stack<string>();
                foreach (Project project in dte.Solution.Projects)
                {
                    AddProject(project, projectsInSolution, projectsToAnalyze, warnings);
                }

                var amountCompleted = 0;
                var total = projectsToAnalyze.Count;

                while (projectsToAnalyze.Count > 0)
                {
                    var projectName = projectsToAnalyze.Pop();

                    UpdateProgress(dte, BuildProgressLabel(projectName, amountCompleted, total), amountCompleted, total);

                    var vsproj = projectsInSolution[projectName];

                    var solutionReferencesToConvert = new Dictionary<string, VSProject>();
                    var thirdPartyReferencesToConvert = new Dictionary<string, string>();

                    foreach (Reference reference in vsproj.References)
                    {
                        // skip if reference is already a project reference
                        if (reference.SourceProject != null)
                            continue;

                        var identity = reference.Identity;

                        // look for source project in found project files
                        if (projectFilesFound.ContainsKey(identity))
                        {
                            VSProject sourceProject;
                            if (projectsInSolution.ContainsKey(identity))
                            {
                                // reference found as solution project
                                sourceProject = projectsInSolution[identity];
                            }
                            else
                            {
                                try
                                {
                                    // referenced assembly does not exist as a project in solution yet
                                    // must find it and then add it to solution

                                    string sourceProjectFile;

                                    // multiple duplicate project files might match reference name

                                    var candidateProjectFiles = projectFilesFound[identity];
                                    if (candidateProjectFiles.Count() > 1)
                                    {
                                        // duplicate project files exist for this reference, ask user to pick one
                                        switch (DupesDialog.Prompt(candidateProjectFiles, projectName, out sourceProjectFile))
                                        {
                                            case DialogResult.Abort: return;
                                            case DialogResult.Ignore: continue;
                                        }

                                        // remember user selection by removing non-selected items
                                        projectFilesFound[identity] = candidateProjectFiles
                                            .Where(file => file == sourceProjectFile)
                                            ;
                                    }
                                    else
                                    {
                                        // only one project file found for this reference
                                        sourceProjectFile = candidateProjectFiles.Single();
                                    }

                                    // add referenced project to solution

                                    UpdateProgress(dte, $"Adding project {sourceProjectFile.Substring(projectsRootPath.Length)}", amountCompleted, total);

                                    try
                                    {
                                        var project = dte.Solution.AddFromFile(sourceProjectFile);

                                        sourceProject = project.Object as VSProject;

                                        if (null == sourceProject)
                                        {
                                            // added project is not supported

                                            try
                                            {
                                                // remove it from solution
                                                dte.Solution.Remove(project);
                                            }
                                            catch
                                            {
                                                warnings.Add($"Could not remove project {project.Name} from solution. Please remove manually.");
                                            }

                                            throw new ApplicationException("Unsupported project type.");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new ApplicationException($"Couldn't add project reference to solution --> {ex.Message}", ex);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    warnings.Add($"Project {projectName} --> Couldn't convert reference to {identity} --> {ex.Message}");
                                    continue;
                                }

                                projectsInSolution.Add(identity, sourceProject);
                                added++;

                                projectsToAnalyze.Push(identity);
                                total++;
                            }

                            solutionReferencesToConvert.Add(identity, sourceProject);
                        }
                    }

                    int i;

                    var solutionRefs = vsproj.References
                        .OfType<Reference>()
                        .Where(r => solutionReferencesToConvert.ContainsKey(r.Identity))
                        .ToArray()
                        ;

                    i = 0;
                    foreach (Reference reference in solutionRefs)
                    {
                        var identity = reference.Identity;

                        var label = $"Project {vsproj.Project.Name}: converting assembly references... ({++i}/{solutionRefs.Length}, {identity})";

                        UpdateProgress(dte, label, amountCompleted, total);

                        // skip if reference was resolved due to projects added to solution
                        if (reference.SourceProject != null)
                            continue;

                        reference.Remove();

                        try
                        {
                            vsproj.References.AddProject(projectsInSolution[identity].Project);
                            converted++;
                        }
                        catch (Exception ex)
                        {
                            warnings.Add($"Project {projectName} --> Couldn't convert reference to {identity} --> {ex.Message}");
                        }
                    }

                    var thirdPartyRefs = vsproj.References
                        .OfType<Reference>()
                        .Where(r => thirdPartyReferencesToConvert.ContainsKey(r.Identity))
                        .ToArray()
                        ;

                    i = 0;
                    foreach (Reference reference in thirdPartyRefs)
                    {
                        var identity = reference.Identity;

                        var label = $"Project {vsproj.Project.Name}: re-mapping third-party references... ({++i}/{thirdPartyRefs.Length}, {identity})";

                        UpdateProgress(dte, label, amountCompleted, total);

                        reference.Remove();

                        try
                        {
                            vsproj.References.Add(thirdPartyReferencesToConvert[identity]);
                            remapped++;
                        }
                        catch (Exception ex)
                        {
                            warnings.Add($"Project {projectName} --> Couldn't remap reference to {identity} --> {ex.Message}");
                        }
                    }

                    amountCompleted++;
                }

                if (total > 0)
                {
                    UpdateProgress(dte, BuildProgressLabel(null, amountCompleted, total), amountCompleted, total);
                }

                MessageBox.Show(BuildSummaryText(added, converted, remapped, warnings), "Convert All References to Project References", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                dte.StatusBar.Progress(false);
                dte.StatusBar.Animate(false, vsStatusAnimation.vsStatusAnimationFind);
                dte.StatusBar.Clear();
                dte.StatusBar.Text = BuildSummaryText(added, converted, remapped, null);
            }
        }

        static void AddProject(Project project, Dictionary<string, VSProject> allprojs, Stack<string> allprojsStack, IList<string> warnings)
        {
            #region Argument validation

            if (null == project)
                throw new ArgumentNullException("project");

            if (null == allprojs)
                throw new ArgumentNullException("allprojs");

            if (null == allprojsStack)
                throw new ArgumentNullException("allprojsStack");

            if (null == warnings)
                throw new ArgumentNullException("warnings");

            #endregion

            var kind = project.Kind.ToUpperInvariant();
            switch (kind)
            {
                case ProjectKinds.vsProjectKindSolutionFolder:
                    AddProjectFolder(project.ProjectItems, allprojs, allprojsStack, warnings);
                    break;
                case "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}":  // Visual Basic
                case "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}":  // Visual C#
                case "{E24C65DC-7377-472b-9ABA-BC803B73C61A}":  // Web Project
                    AddVSProject(project.Object as VSProject, allprojs, allprojsStack);
                    break;
                default:
                    warnings.Add("Ignoring project " + project.Name + ": Unsupported kind.");
                    break;
            }
        }

        static void AddProjectFolder(ProjectItems items, Dictionary<string, VSProject> allprojs, Stack<string> allprojsStack, IList<string> warnings)
        {
            #region Argument validation

            if (null == items)
                throw new ArgumentNullException("items");

            if (null == allprojs)
                throw new ArgumentNullException("allprojs");

            if (null == allprojsStack)
                throw new ArgumentNullException("allprojsStack");

            if (null == warnings)
                throw new ArgumentNullException("warnings");

            #endregion

            foreach (ProjectItem item in items)
                if (item.SubProject != null)
                    AddProject(item.SubProject, allprojs, allprojsStack, warnings);
        }

        static void AddVSProject(VSProject vsproj, Dictionary<string, VSProject> allprojs, Stack<string> allprojsStack)
        {
            #region Argument validation

            if (null == vsproj)
                throw new ArgumentNullException("vsproj");

            if (null == allprojs)
                throw new ArgumentNullException("allprojs");

            if (null == allprojsStack)
                throw new ArgumentNullException("allprojsStack");

            #endregion

            var identity = vsproj.Project.Name;

            allprojs.Add(identity, vsproj);
            allprojsStack.Push(identity);
        }
        
        static string BuildSummaryText(int added, int converted, int remapped, IEnumerable<string> warnings)
        {
            return string.Format(
                "Finished.{0}{0}" +
                "{1} projects added, " +
                "{2} assembly references converted to project references, " +
                "{3} third-party assembly references remapped." +
                "{4}",
                Environment.NewLine,
                added,
                converted,
                remapped,
                warnings == null || !warnings.Any()
                    ? string.Empty
                    : string.Format(
                        "{0}{0}Warnings:{0}{1}",
                        Environment.NewLine,
                        string.Join(Environment.NewLine, warnings.Select(w => "* " + w))
                        )
                );
        }

        static string BuildProgressLabel(string projectName, int amountCompleted, int total)
        {
            return string.Format(
                "{0}: adding required projects and updating references... ({1}/{2})",
                string.IsNullOrEmpty(projectName) ? "Finished" : ("Project " + projectName),
                projectName,
                amountCompleted,
                total
                );
        }

        static void UpdateProgress(_DTE dte, string label, int amountCompleted, int total)
        {
            #region Argument validation

            if (amountCompleted < 0)
                throw new ArgumentOutOfRangeException("total", "Progress Amount Completed must be zero or greater.");

            if (total <= 0)
                throw new ArgumentOutOfRangeException("total", "Progress Total must be greater than zero.");

            if (amountCompleted > total)
                throw new ArgumentOutOfRangeException("amountCompleted", "Progress Amount Completed cannot be greater than Total.");

            #endregion

            dte.StatusBar.Progress(true, label, amountCompleted, total);
            Application.DoEvents();
        }

        static void UpdateStatus(_DTE dte, string text)
        {
            dte.StatusBar.Text = text;
            Application.DoEvents();
        }

        static string GetProjectsRootPath(string defaultFolder = null)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Enter root folder to search for Projects";
                dialog.SelectedPath = defaultFolder;
                return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
            }
        }

        static string GetAssembliesRootPath()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Enter root folder to search for 3rd-party Assemblies";
                return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
            }
        }

        static IEnumerable<string> GetProjectFilters()
        {
            var iniFile = new IniFile();
            var filters = iniFile.Read("ProjectNameFilters"); // "CIS.*|Viterra.*|ViterraWebControls.*";
            var text = "Please specify reference names to search for.\n\nValid wildcards characters: * ?\nPattern separator: |\n";
            var caption = "Project Name Filters";

            var newFilters = filters;
            var result = PromptDialog.Prompt(text, caption, filters, out newFilters) == DialogResult.OK
                ? newFilters.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                : null
                ;

            if (newFilters != filters)
            {
                iniFile.Write("ProjectNameFilters", newFilters);
            }

            return result;
        }

        static IDictionary<string, IEnumerable<string>> FindProjectFiles(string rootFolder, IEnumerable<string> patterns)
        {
            #region Argument validation

            if (string.IsNullOrEmpty(rootFolder))
                throw new ArgumentNullException("rootFolder");

            if (null == patterns)
                throw new ArgumentNullException("patterns");

            if (!Directory.Exists(rootFolder))
                throw new ArgumentException("Specified root folder '" + rootFolder + "' does not exist.");

            if (!patterns.Any())
                throw new ArgumentException("No patterns specified.");

            #endregion

            var dic = new ConcurrentDictionary<string, IEnumerable<string>>();

            patterns.AsParallel().ForAll(pattern =>
            {
                AddFiles(rootFolder, pattern + ".??proj", dic);
            });

            return dic;
        }

        static void AddFiles(string rootFolder, string pattern, ConcurrentDictionary<string, IEnumerable<string>> dic)
        {
            Directory.EnumerateFiles(rootFolder, pattern, SearchOption.AllDirectories)
            .AsParallel()
            .ForAll(file =>
            {
                var identity = Path.GetFileNameWithoutExtension(file);
                var bag = (ConcurrentBag<string>)dic.GetOrAdd(identity, new ConcurrentBag<string>());
                bag.Add(file);
            });
        }
    }
}
