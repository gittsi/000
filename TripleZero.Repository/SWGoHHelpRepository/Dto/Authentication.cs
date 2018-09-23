using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TripleZero.Repository.SWGoHHelp.Dto;

namespace TripleZero.Repository.SWGoHHelpRepository.Authentication
{
    public class Authentication
    {
        public string _client_id = "123";
        public string _client_secret = "ABC";
        public string _protocol = "";
        public string _host = "";
        public string _port = "";

        private IMemoryCache cache;
        SWGoHHelpSettings _settings;
        public Authentication(SWGoHHelpSettings settings, IMemoryCache cache)
        {
            this.cache = cache;
            _settings = settings;
        }

        public string GetToken()
        {
            var token = cache.Get<string>("access_token");

            if (!string.IsNullOrEmpty(token)) return token;

            string protocol = string.IsNullOrEmpty(_protocol) ? "https" : _protocol;
            string host = string.IsNullOrEmpty(_host) ? "api.swgoh.help" : _host;
            string port = string.IsNullOrEmpty(_port) ? "" : _port;

            var url = protocol + "://" + host + port;
            var signIn = url + "/auth/signin/";

            var user = "username=" + _settings.UserName;
            user += "&password=" + _settings.Password;
            user += "&grant_type=password";
            user += "&client_id=" + _client_id;
            user += "&client_secret=" + _client_secret;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(signIn);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteArray = Encoding.UTF8.GetBytes(user);
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var loginresponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                var loginResponseObject = JsonConvert.DeserializeObject<LoginResponse>(loginresponse);
                token = loginResponseObject.access_token;

                cache.Set<string>("access_token",token, new TimeSpan(0,50,0));
                return token;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
