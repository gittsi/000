using Newtonsoft.Json;
using SWGoH.Model.Model.Settings;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TripleZero.Core.Caching;
using TripleZero.Repository.SWGoHHelp.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace TripleZero.Repository.SWGoHHelpRepository
{
    public class Authentication
    {
        public string _client_id = "123";
        public string _client_secret = "ABC";
        public string _protocol = "";
        public string _host = "";
        public string _port = "";
        private MemoryCache _myCache;

        private CacheClient _cacheClient;
        SWGoHHelpSettings _settings;
        public Authentication(SWGoHHelpSettings settings, CacheClient cacheClient)
        {
            _cacheClient = cacheClient;
            _settings = settings;
        }

        public Authentication(SWGoHHelpSettings settings, MemoryCache myCache)
        {            
            _settings = settings;
            _myCache = myCache;
        }


        public async Task<string> GetToken()
        {
            

            var token = "";
            string functionName = "SWGoHHelp";
            string key = "token";

            if (_myCache != null)
            {
                token = (string)_myCache.Get(functionName + key);
                if(token!=null) return token;
            }

            var objCache = _cacheClient?.GetDataFromRepositoryCache(functionName, key);
            if (objCache != null)
            {
                token = (string)objCache;
                return token;
            }

            Console.WriteLine($": Accessing SWGoH API authentication");

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
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var loginresponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                var loginResponseObject = JsonConvert.DeserializeObject<LoginResponse>(loginresponse);
                token = loginResponseObject.access_token;

                //load to cache
                try
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        _myCache.Set(functionName + key, token, new TimeSpan(0, 1, 50, 0, 0));
                        var b = await _cacheClient?.AddToRepositoryCache(functionName, key, token, _settings.TokenCachingInMinutes);
                    }                    
                }
                catch (Exception ex)
                {

                }

                return token;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
