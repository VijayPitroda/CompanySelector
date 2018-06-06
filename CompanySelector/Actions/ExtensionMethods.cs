using CompanySelector.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace CompanySelector.Actions
{
    /// <summary>
    /// Get Hosted path to file extension :)
    /// </summary>
    public static class GetIconExtension
    {
        public static string GetIconPath(this string iconname, IHostingEnvironment env)
        {
            string logopath = "";
            if (iconname == "cnk")
            {
                logopath = env.WebRootFileProvider.GetFileInfo("images/cnk.jpg")?.PhysicalPath;
            }
            if (iconname == "mastek")
            {
                logopath = env.WebRootFileProvider.GetFileInfo("images/Mastek.jpg")?.PhysicalPath;
            }
            return logopath;
        }
    }

    public static class GetThemeExtension
    {
        public static string GetThemeFilePath(this string themeFile, IHostingEnvironment env)
        {
            string tpath = "";
            if (themeFile == "t1")
            {
                tpath = env.WebRootFileProvider.GetFileInfo("content/t1.xml")?.PhysicalPath;
            }
            if (themeFile == "t2")
            {
                tpath = env.WebRootFileProvider.GetFileInfo("content/t2.xml")?.PhysicalPath;
            }
            return tpath;
        }
    }

    /// <summary>
    /// Run the android build
    /// </summary>
    public static class TriggerBuildExtension
    {
        public static void BuildAndroidApk(this string Command)
        {
            ProcessStartInfo ProcessInfo = new ProcessStartInfo
            {
                WorkingDirectory = ProjectPathConst.p2,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                //RedirectStandardInput = true,
                UseShellExecute = true,
                Arguments = "/K " + Command
            };
            Process Process;
            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();
        }

    }
}
