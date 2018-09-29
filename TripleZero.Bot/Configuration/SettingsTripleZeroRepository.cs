using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Bot.Settings
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
    }

    public class SWGoHHelpSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public partial class CachingSettings
    {
        public int RepositoryCachingInMinutes { get; set; }
    }
}
