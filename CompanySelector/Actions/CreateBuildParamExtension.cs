using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySelector.Actions
{
    public static class CreateBuildParamExtension
    {
        public static string GetParams(this string selected)
        {
            var p = $"gradlew assemble{selected}Debug";
            return p;
        }
    }

    
}
