using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySelector.Models
{
    public class TestModel
    {
        public List<string> layouts { get; set; }
        
        public List<string> getList()
        {
            layouts = new List<string>();
            for(int i=0; i<6; i++)
            {
                layouts.Add($"m + {i+1}");
            }
            return layouts;
        }
    }
}
