using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ResourcesGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnRootFolder_Click(object sender, RoutedEventArgs e)
        {
            var directoryDialog = new System.Windows.Forms.FolderBrowserDialog();
            directoryDialog.SelectedPath = Properties.Settings.Default.lastSourceFolderPath;
            if (directoryDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.lastSourceFolderPath = directoryDialog.SelectedPath;
                Properties.Settings.Default.Save();

                var exceptionName = txtExceptionName.Text;
                if(string.IsNullOrWhiteSpace(exceptionName))
                    throw new Exception("Exception Name is required to generate Resource File");

                var keys = new List<string>();
                var files = Directory.GetFiles(directoryDialog.SelectedPath, "*.cs", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    using (var sr = File.OpenText(file))
                    {
                        var fileText = sr.ReadToEnd();

                        //throw new BookizerException<Business>(x => x.Id, ViolationType.NotFound);
                        var patt1 = exceptionName + @"\<(\w+)\>\([a-z] ?\=\> ?[a-z]\.(.+), *ViolationType\.(\w+)";
                        foreach (Match match in Regex.Matches(fileText, patt1))
                        {
                            var value = match.Groups[1].Value;
                            value = value + "_" + match.Groups[2].Value.Replace(".", "_");
                            value = value + "_" + match.Groups[3].Value;
                            keys.Add(value);
                        }

                        //ViolationHandler.AddViolation(x => x.Email, ViolationType.Required);
                        var patt2 = @"[vV]iolationHandler.AddViolation\([a-z] ?\=\> ?[a-z]\.(.+), *ViolationType\.(\w+)";
                        var classNamePattern = @"class (?<className>\w*)";
                        var className = Regex.Match(fileText, classNamePattern).Groups["className"].Value;
                        foreach (Match match in Regex.Matches(fileText, patt2))
                        {
                            var value = className;
                            value = value + "_" + match.Groups[1].Value.Replace(".", "_");
                            value = value + "_" + match.Groups[2].Value;
                            keys.Add(value);
                        }

                        var patt3 = @"[vV]iolationHandler.AddViolation\<(\w+)\>\([a-z] ?\=\> ?[a-z]\.(.+),.*ViolationType\.(\w+)";
                        foreach (Match match in Regex.Matches(fileText, patt3))
                        {
                            var value = match.Groups[1].Value;
                            value = value + "_" + match.Groups[2].Value.Replace(".", "_");
                            value = value + "_" + match.Groups[3].Value;
                            keys.Add(value);
                        }
                    }
                }

                var destinationPathDialog = new System.Windows.Forms.FolderBrowserDialog();
                destinationPathDialog.SelectedPath = Properties.Settings.Default.lastTargetFolderPath;
                if (destinationPathDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.lastTargetFolderPath = destinationPathDialog.SelectedPath;
                    Properties.Settings.Default.Save();

                    CreateRes(keys.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList(), destinationPathDialog.SelectedPath, "ViolationKeys.resx");
                }
            }
        }


        public static void CreateRes(List<string> resourceKeys, string destinationPath, string fileName)
        {
            var directory = new DirectoryInfo(destinationPath);
            var oldResFile = directory.GetFiles("*.resx").FirstOrDefault();
            Dictionary<string, string> resourceMap = new Dictionary<string, string>();

            if (oldResFile != null)
            {
                ResXResourceReader rsxr = new ResXResourceReader(oldResFile.FullName);
                foreach (DictionaryEntry d in rsxr)
                {
                    resourceMap.Add(d.Key.ToString(), d.Value.ToString());
                }
                rsxr.Close();
            }

            using (var writer = new ResXResourceWriter(System.IO.Path.Combine(destinationPath, fileName)))
            {
                foreach (var key in resourceKeys.Where(k => !resourceMap.ContainsKey(k)))
                {
                    var keyArray = key.Split('_');
                    var entityType = keyArray.First();
                    var violationType = keyArray.Last();
                    var propertyPathArray = keyArray.Skip(1).Take(keyArray.Length - 2).ToList();
                    var propertyPath = string.Join(".", propertyPathArray);
                    writer.AddResource(key, getAutomateErrorMessage(propertyPath, violationType, entityType));
                }
                writer.Generate();
                writer.Close();
            }
            var deleted = string.Join(Environment.NewLine, resourceMap.Where(ok => resourceKeys.All(nk => nk != ok.Key)).Select(k => k.Key));
            File.WriteAllText(System.IO.Path.Combine(destinationPath, "deleted.txt"), deleted);
        }



        private static string getAutomateErrorMessage(string propertyPath, string violationType, string entityType)
        {
            var messageBuilder = new StringBuilder();
            switch (violationType)
            {
                case "Required":
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The Field ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                        messageBuilder.Append(" is required");
                    }
                    break;
                case "Duplicated":
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("There is already entity with the same ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                    }
                    messageBuilder.Append(" value. This field should be unique");
                    break;
                case "Invalid":
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The value of the ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                    }
                    if (entityType != null)
                        messageBuilder.Append(entityType);
                    messageBuilder.Append(" is invalid");
                    break;
                case "MaxLength":
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The length of the ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                        messageBuilder.Append(" is too long");
                    }
                    break;
                case "MinLength":
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The length of the ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                        messageBuilder.Append(" is too small");
                    }
                    break;
                default:
                    messageBuilder.Append("There is an error with ");
                    /*if (entityType != null)
                        messageBuilder.Append(entityType + " the field ");*/
                    if (!string.IsNullOrWhiteSpace(propertyPath))
                        messageBuilder.Append("[" + propertyPath + "]");
                    else if (!string.IsNullOrWhiteSpace(entityType))
                    {
                        messageBuilder.Append("[" + entityType + "]");
                    }
                    messageBuilder.Append(". It is [" + violationType.ToString() + "]");
                    break;
            }
            messageBuilder.Append(" !.");
            return messageBuilder.ToString();
        }
    }
}
