using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model.Model.Settings
{
    public class SWGoHHelpSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Protocol => "https";
        public string Host => "api.swgoh.help";
        public string Port => "";
        public short TokenCachingInMinutes { get; set; }
    }
}
