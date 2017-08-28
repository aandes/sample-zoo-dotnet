using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace sample_zoo_dotnet.Utils
{
    // faciliates reading content form a JSON Object
    // can be improved to become a complete abstraction layer
    public class ContentHelper
    {
        // build and instance from a JObject
        public ContentHelper(JObject source)
        {
            _source = source;
        }

        // build and instance from a JToken
        public ContentHelper(JToken source)
        {
            _source = source.Value<JObject>();
        }

        // loads the value at the given path (e.g. "deep.path.prop") as a string
        // returns the dummy if the property is null, empty or is not set
        public string Text(string path, string dummy = "Insert Content") 
        { 
            JToken token = _source.SelectToken(path);

            if (token == null) {
                return dummy;
            }

            string value = token.ToString();

            return string.IsNullOrEmpty(value) ? dummy : value;
        }

        // casts the result of #Text to an HtmlString and retuns it
        public HtmlString Html(string path, string dummy = "Insert Content")
        {
            string value = Text(path, dummy);
            return new HtmlString(value == null ? String.Empty: value);
        }

        // loads the JSON Object value at the given path (e.g. "deep.path.prop")
        // returns an empty object if the property is null, empty or is not set
        public JObject Object(string path, JObject dummy = null)
        {
            JToken token = _source.SelectToken(path);

            if (token == null)
            {
                return dummy == null ? new JObject() : dummy;
            }

            return token.Value<JObject>();
        }

        // casts the result of #Object to a ContentHelper and retuns it
        public ContentHelper Content(string path, JObject dummy = null)
        {
            return new ContentHelper(Object(path, dummy));
        }

        #region Private members
        private JObject _source;
        #endregion

    }
}