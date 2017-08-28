using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sample_zoo_dotnet.Utils;

namespace sample_zoo_dotnet.Models
{
    public class CmsComponentModel
    {
        public CmsComponentModel() { }

        public ContentHelper cms 
        {
            get { return _cms;  }
            set { _cms = value; }
        }

        public string key
        {
            get { return _key; }
            set { _key = value; }
        }

        private ContentHelper _cms;
        private string _key;
    }
}