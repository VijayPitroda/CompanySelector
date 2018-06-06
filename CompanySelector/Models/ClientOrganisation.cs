using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySelector.Models
{
    /// <summary>
    /// Android configure UI
    /// </summary>
    public class ClientOrganisation
    {
        public string company { get; set; }

        public bool update { get; set; }

        public string selectIcon { get; set;}

        public LayoutModel layoutmodel { get; set; }

        //public string layout { get; set; }

        public AppFeatures features {get; set;}

        
    }

}
