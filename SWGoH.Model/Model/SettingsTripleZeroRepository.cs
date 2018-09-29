using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model
{
    public class SettingsTripleZeroRepository
    {
        public MongoDBSettings MongoDBSettings { get; set; }
        public CachingSettings CachingSettings { get; set; }
        public SWGoHHelpSettings SWGoHHelpSettings { get; set; }
    }
    public class MongoDBSettings
    {
        public string ApiKey { get; set; }
        public string DB { get; set; }
        public string ConnectionString { get; set; }
    }

    public class SWGoHHelpSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Protocol => "https";
        public string Host => "api.swgoh.help";
        public string Port => "";
    }
    public partial class CachingSettings
    {
        public int RepositoryCachingInMinutes { get; set; }
    }
}
