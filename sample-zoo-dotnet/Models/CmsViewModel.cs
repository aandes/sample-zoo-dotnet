using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using sample_zoo_dotnet.Utils;
using System.ComponentModel.DataAnnotations;

namespace sample_zoo_dotnet.Models
{
    // this simple data model contains a ContentHelper 
    // instance and shortcuts to some of its methods
    public class CmsViewModel
    {
        [Required]
        public ContentHelper cms { get; set; }

        // gets a plain text from the ContentHelper
        public string Text(string path, string dummy = "Insert Content")
        {
            return cms.Text(path, dummy);
        }

        // gets an HTML text from the ContentHelper
        public HtmlString Html(string path, string dummy = "Insert Content")
        {
            return cms.Html(path, dummy);
        }

        // gets a JSON Object from the ContentHelper
        public JObject Object(string path, JObject dummy = null)
        {
            return cms.Object(path, dummy);
        }

        // creates a new ContentHelper based the
        // JSON Object at the given path (a sub-view, of sort)
        public ContentHelper Content(string path, JObject dummy = null)
        {
            return cms.Content(path, dummy);
        }
    }
}