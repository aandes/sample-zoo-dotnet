using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;

namespace sample_zoo_dotnet.Utils
{
    public class Cms
    {
        static Cms () {

            NameValueCollection settings = WebConfigurationManager.AppSettings;

            // is the application is authoring mode
            _autoring = Boolean.Parse(settings["cmsAuthoring"]);

            // a placeholder image that signifies: no image
            _imgPlaceholder = settings["cmsImgPlaceholder"];

            // name of the target application in the cms
            _mirror = settings["cmsMirror"];

            // build rapid url
            _rapidUrl = String.Format("{0}:{1}/~rapid/edit/{2}",
                settings["cmsHost"], settings["cmsRapidPort"], _mirror);

            // prepare cms content urlLoaderer
            urlLoader = new UrlLoader(settings["cmsUser"], settings["cmsPass"]);

            // whether the target CMS instance requires authentication,
            // this is usually false on a publish instance for example
            urlLoader.Authenticate = Boolean.Parse(settings["cmsRequiresAuthentication"]);

            // prepare cms content urls
            _cmsOrigin = settings["cmsHost"] + ":" + settings["cmsPort"];
            _contentFormat = settings["cmsContentFormat"];
            
        }

        public static HtmlString Edit (string value) {
            
            return new HtmlString(_autoring ?
                "data-designmode=\"" + value + "\"" : String.Empty);

        }

        public static ContentHelper Content (string action, string key = "")
        {

            string json = urlLoader.Fetch(
                // {0}: cmsOrigin {1}: cmsMirror, {2}: current action, {3}: content key (optional)
                string.Format(_contentFormat, _cmsOrigin, _mirror, action, key));

            JObject obj = json == null ? new JObject() : JObject.Parse(json);
            
            return new ContentHelper(obj);
        
        }

        public static bool Authoring
        {
            get { return _autoring; }
        }

        public static string RapidUrl
        {
            get { return _rapidUrl; }
        }

        public static string ImgPlaceholder
        {
            get { return _imgPlaceholder; }
        }

        public static string Origin
        {
            get { return _cmsOrigin; }
        }

        private static UrlLoader urlLoader;

        private static bool _autoring;

        private static string _mirror,
                              _rapidUrl,
                              _cmsOrigin,
                              _contentFormat,
                              _imgPlaceholder;

    }
}