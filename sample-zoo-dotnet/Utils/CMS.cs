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
        // loads and parses the cms settings
        static Cms () {

            NameValueCollection settings = WebConfigurationManager.AppSettings;

            // is the application is authoring mode
            _autoring = Boolean.Parse(settings["cmsAuthoring"]);

            // a placeholder image that signifies: no image
            _imgPlaceholder = settings["cmsImgPlaceholder"];

            // name of the target application in the cms
            _mirror = settings["cmsMirror"];

            // prepare cms content urlLoaderer
            urlLoader = new UrlLoader(settings["cmsUser"], settings["cmsPass"]);

            // whether the target CMS instance requires authentication,
            // this is usually false on a publish instance for example
            urlLoader.Authenticate = Boolean.Parse(settings["cmsRequiresAuthentication"]);

            // prepare cms content urls
            _cmsOrigin = settings["cmsHost"] + ":" + settings["cmsPort"];
            _contentFormat = settings["cmsContentFormat"];

            // cache rapid script url template
            _cmsRapidScript = settings["cmsRapidScript"];

            // optional: this is for this application only
            // whether the content author is allowed to introduce 
            // new items on the pages
            _allowComponentsInsertion = Boolean.Parse(
                settings["cmsAllowComponentsInsertion"]);
        }

        // outputs the data-desigmode tag if the app is in authoring mode
        public static HtmlString Edit (string value) {
            
            return new HtmlString(_autoring ?
                "data-designmode=\"" + value + "\"" : String.Empty);

        }
        
        // gets the content for the provided action. if a key is not passed
        // content for the entire page is returned
        public static ContentHelper Content (string action, string key = "")
        {
            // build content path for the provided action
            string contentPath = string.Format(_contentFormat,
                // {0}: cmsOrigin 
                // {1}: cmsMirror
                // {2}: current action
                // {3}: content key (optional)
                _cmsOrigin, _mirror, action, key);

            // make the request
            string json = urlLoader.Fetch(contentPath);

            JObject obj;

            if (json == null) {
                // no content? use null object pattern
                obj = new JObject();
                // but warn the admin
                Log.Warning(string.Format("No content found at '{0}'!", 
                    contentPath));
            }
            else {
                // fine, parse the content
                obj = JObject.Parse(json);  
            }
            
            // while we could have simply returned the JSON Object
            // it's best practice to abstract it in a ContentHelper
            return new ContentHelper(obj);
        }

        // tells whether this app is in authoring mode
        public static bool Authoring
        {
            get { return _autoring; }
        }

        // returns RAPID's script tag src value
        public static string RapidUrl
        {
            get
            {
                // optional: pass the current locale as a context path to RAPID
                // for example:  "/en/us"  or  "/fr"  
                string currentLocale = "";
                return String.Format(_cmsRapidScript, _cmsOrigin, _mirror, currentLocale);
            }

        }

        // returns the path of a dummy placeholder image in the CMS
        public static string ImgPlaceholder
        {
            get { return _imgPlaceholder; }
        }

        // returns the CMS' scheme + hostname + port 
        public static string Origin
        {
            get { return _cmsOrigin; }
        }

        // tells whether to allows content authors to add more animals to the pages
        // this is optional ans specific to this app. has nothing to do with RAPID or AEM
        public static bool AllowComponentsInsertion
        {
            get { return _autoring && _allowComponentsInsertion; }
        }

        #region Private members
        private static UrlLoader urlLoader;

        private static bool _autoring,
                            _allowComponentsInsertion;

        private static string _mirror,
                              _cmsOrigin,
                              _contentFormat,
                              _cmsRapidScript,
                              _imgPlaceholder;
        #endregion
    }
}