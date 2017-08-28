using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;

namespace sample_zoo_dotnet.Utils
{
    // simple URL Loader... but more than enough to work with AEM/Sling
    public class UrlLoader
    {
        
        // Creates the instance
        public UrlLoader(string user = null, string pass = null)
        {
            _user = user;
            _pass = pass;
        }

        // Load and return text at the provided URL
        public string Fetch(string url)
        {
            HttpClientHandler handler;
            string responseString;
            
            try {

                if (_authenticate) {
                    // create a handler for my http client with my username/password
                    using (handler = new HttpClientHandler
                    {
                        Credentials = new System.Net.NetworkCredential(_user, _pass)
                    }) {
                        // initialize http client object
                        using (var client = new HttpClient(handler))
                        {
                            responseString = Request(client, url);
                        }
                    };
                }
                else {
                    // initialize http client object un-authenticated
                    using (var client = new HttpClient())
                    {
                        responseString = Request(client, url);
                    }
                }
            }
            catch (Exception e) {
                Log.Error(e);
                responseString = null;
            }

            return responseString;
        }

        // tells whether the request should pass the provided credentials
        public bool Authenticate {
            get { return _authenticate;  }
            set { _authenticate = value; }
        }

        // makes an HTTP request the provided URL using this client
        private string Request (HttpClient client, string url) {
            
            string responseString = null;
            // get the response
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                responseString = responseContent.ReadAsStringAsync().Result;
            }

            return responseString;
        
        }

        #region Private members
        private bool _authenticate;
        
        private string _user,
                       _pass;
        #endregion
    }

}