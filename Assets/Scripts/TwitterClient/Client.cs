using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;
using System.Text;


namespace teruzuki.Twitter
{
    public class Client
    {
        private static Client instance;

        private OAuth.Manager oauth;

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }
                return instance;
            }
        }

        private Client()
        {
            oauth = new OAuth.Manager();
            oauth["consumer_key"] = "OcbDuSiWrHYWU2RFgdWyV61F8";
            oauth["consumer_secret"] = "7fNW3QITGNFQAisvtkk8yaHdXkx5j7mxM2rEJShUeqxbwZEDHZ";
        }

        public static string GetRequestToken()
        {
            var res = Instance.oauth.AcquireRequestToken("https://api.twitter.com/oauth/request_token", "POST");
            return "https://api.twitter.com/oauth/authenticate?oauth_token=" + res["oauth_token"];
        }

        public static void GetAccessToken(string pin)
        {
            var res = Instance.oauth.AcquireAccessToken("https://api.twitter.com/oauth/access_token", "POST", pin);

            Instance.oauth["token"] = res["oauth_token"];
            Instance.oauth["token_secret"] = res["oauth_token_secret"];
        }

        public string Get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "GET";
            req.ServicePoint.Expect100Continue = false;
            req.ContentType = "x-www-form-urlencoded";

            req.Headers["Authorization"] = oauth.GenerateAuthzHeader(url, "GET");

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            using (var reader = new StreamReader(res.GetResponseStream()))
            {
                string value = reader.ReadToEnd();
                return value;
            }
        }
        public string BuildURL(string baseurl, string key, string value)
        {
            NameValueCollection parameter = new NameValueCollection();
            parameter.Add(key, value);
            return this.BuildURL(baseurl, parameter);
        }

        public string BuildURL(string baseurl, NameValueCollection parameters)
        {
            StringBuilder q = new StringBuilder();

            foreach (string key in parameters)
            {
                q.Append((q.Length == 0) ? '?' : '&');
                q.Append(key);
                q.Append('=');
                q.Append(parameters[key]);
            }

            return baseurl + q.ToString();
        }

    }
}
