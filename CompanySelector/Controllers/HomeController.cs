using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CompanySelector.Models;
using CompanySelector.Actions;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace CompanySelector.Controllers
{
    public class HomeController : Controller
    {
        private ClientOrganisation clientOrganisation;

        private Utils utils;
        private readonly IHostingEnvironment env;

        public HomeController(IHostingEnvironment env)
        {
            clientOrganisation = new ClientOrganisation();

            utils = new Utils();
            this.env = env;
        }

        public IActionResult Index()
        {
            return View(clientOrganisation);
        }

        #region Scafolding Code
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        /// <summary>
        /// Select and send the selected company to trigger Android build
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuildAndroid(ClientOrganisation selected)
        {
            //Brand or Company name 
            utils.writeCompanyName(selected.company);
            //update or New app
            utils.modifyGradleBuild(selected.update, selected.company);
            //Selected Icon
            utils.selectLogo(selected.selectIcon, env);
            //select the layout
            utils.selectLayout(selected.layoutmodel, env);
            //Enable or disable features 
            utils.writeToFile(selected.features);
            //Build Android
            utils.triggerBuild();
            return RedirectToAction("BuildDone");
        }

        public IActionResult BuildDone()
        {
            return View();
        }

        public IActionResult TestView()
        {
            TestModel tm = new TestModel();

            return View(tm);
        }

        public string getIconPath(string iconname)
        {
            string logopath ="";
            if (iconname == "cnk")
            {
                logopath = env.WebRootFileProvider.GetFileInfo("images/CNK.png")?.PhysicalPath;
            }
            if(iconname == "mastek")
            {
                logopath = env.WebRootFileProvider.GetFileInfo("images/Mastek.png")?.PhysicalPath;
            }
            return logopath;
        }
    }

}
