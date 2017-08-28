using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sample_zoo_dotnet.Utils;

namespace sample_zoo_dotnet.Models
{
    // this is used for the components, which are 
    // simply the partial views with a content key
    public class CmsComponentModel: CmsViewModel
    {
        public string key { get; set; }
    }
}