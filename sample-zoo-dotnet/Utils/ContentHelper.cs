using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace sample_zoo_dotnet.Utils
{
    public class ContentHelper
    {
        public ContentHelper(JObject source)
        {
            _source = source;
        }

        public ContentHelper(JToken source)
        {
            _source = source.Value<JObject>();
        }

        public string Text(string path, string defaultValue = "Insert Content") 
        { 
            JToken token = _source.SelectToken(path);

            if (token == null) {
                return defaultValue;
            }

            string value = token.ToString();

            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public HtmlString Html(string path, string defaultValue = "Insert Content")
        {
            string value = Text(path, defaultValue);
            return new HtmlString(value == null ? String.Empty: value);
        }

        public JObject Object(string path, JObject defaultValue = null)
        {
            JToken token = _source.SelectToken(path);

            if (token == null)
            {
                return defaultValue == null ? new JObject() : defaultValue;
            }

            return token.Value<JObject>();
        }

        public ContentHelper Content(string path, JObject defaultValue = null)
        {
            return new ContentHelper(Object(path, defaultValue));
        }

        private JObject _source;

    }
}