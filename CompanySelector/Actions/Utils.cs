using CompanySelector.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CompanySelector.Actions
{
    public class Utils
    {
        /// <summary>
        /// Change Company Name in the App :)
        /// Ref - XMlModify project
        /// </summary>
        /// <param name="cname"></param>
        public void writeCompanyName(string cname)
        {
            if (cname != null || cname != "")
            {
                XElement xdoc = XElement.Load(ProjectPathConst.cnamePath);
                xdoc.Elements().Where(c => c.FirstAttribute.Value == "app_name").FirstOrDefault().SetValue(cname);
                xdoc.Save(ProjectPathConst.cnamePath);
            }
        }

        /// <summary>
        /// modify Gradle build id to generate new instance 
        /// </summary>
        /// <param name="newInstance"></param>
        /// <param name="company"></param>
        public void modifyGradleBuild(bool newInstance, string company)
        {
            if(newInstance)
            {
                string mstr = Regex.Replace(company, @"[^0-9a-zA-Z]+", "");
                string my_String = mstr.ToLower();

                var data = File.ReadAllLines(ProjectPathConst.buildgradle);
                data[5] = ProjectPathConst.appid + "com." + my_String; ;
                File.WriteAllLines(ProjectPathConst.buildgradle, data);
            }

        }

        /// <summary>
        /// Select brand logo and replace :)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="env"></param>
        public void selectLogo(string icon, IHostingEnvironment env)
        {
            string filename = icon + ".jpg";
            string sourceFile = icon.GetIconPath(env);
            string targetFile = ProjectPathConst.iconTargetPath + "app_icon.jpg";
            if(File.Exists(targetFile))
            {
                File.Delete(targetFile);
            }
            File.Copy(sourceFile, ProjectPathConst.iconTargetPath + filename, true);
            File.Move(ProjectPathConst.iconTargetPath + filename, targetFile);
        }

        /// <summary>
        /// Select Layout from catalog
        /// </summary>
        /// <param name="layoutmodel"></param>
        /// <param name="env"></param>
        public void selectLayout(LayoutModel layoutmodel, IHostingEnvironment env)
        {
            string theme = layoutmodel.themenumber;
            string layout = layoutmodel.layout;
            //copy the theme file 
            copyThemeFile(theme, env);
            //edit the android manifest
            editAndroidManifest(layout);
        }

        /// <summary>
        /// ref : selectLayout(LayoutModel layoutmodel, IHostingEnvironment env)
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="env"></param>
        public void copyThemeFile(string theme,IHostingEnvironment env)
        {
            string fname = "colors.xml";
            string target = ProjectPathConst.colorsPath;
            string source = theme.GetThemeFilePath(env);
            if(File.Exists(target+fname))
            {
                File.Delete(target + fname);
            }
            File.Copy(source, ProjectPathConst.colorsPath + theme +".xml", true);
            File.Move(ProjectPathConst.colorsPath + theme + ".xml", ProjectPathConst.colorsPath+fname);
        }

        /// <summary>
        /// Chnage the activity number in the android manifest :)
        /// </summary>
        /// <param name="layout"></param>
        public void editAndroidManifest(string layout)
        {
            //get layout number
            var resultstr = Regex.Match(layout, @"\d+").Value;
            //Create android activity name 
            var activity = $".activity.MainActivity{resultstr}";
            //load manifest file for edit
            var doc = XDocument.Load(ProjectPathConst.manifestPath);
            XNamespace android = "http://schemas.android.com/apk/res/android";
            doc.Elements("manifest").Elements("application").Elements("activity")
                .FirstOrDefault().FirstAttribute.Value = activity;
            
            /*for testing only */
            //var s = doc.Elements("manifest").Elements("application").Elements("activity")
            //    .FirstOrDefault().FirstAttribute.Value;

            doc.Save(ProjectPathConst.manifestPath);
        }

        /// <summary>
        /// Generate on_off_property_file and place in Android project asset folder :)
        /// Ref- GenerateJson project
        /// </summary>
        /// <param name="data"></param>
        public void writeToFile(AppFeatures data)
        {
            //data.mainScreen.itinerary = true;
            string filename = "on_off_property_file";
            if (File.Exists(ProjectPathConst.assetPath + filename))
            {
                File.Delete(ProjectPathConst.assetPath + filename);
                using (StreamWriter file = File.CreateText(ProjectPathConst.assetPath + filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, data);
                }
            }
        }

        /// <summary>
        /// Trigger build 
        /// </summary>
        public void triggerBuild()
        {
            string cmd = "gradlew clean assembleDebug";
            cmd.BuildAndroidApk();
        }
    }

   
}
