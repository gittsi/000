using SWGoH.Model.Model.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model.Settings.TripleZeroBot
{
    public class SettingsTripleZeroBot
    {
        public GeneralSettings GeneralSettings { get; set; }
        public DiscordSettings DiscordSettings { get; set; }        
        public CachingSettings CachingSettings { get; set; }
    }    
    public class DiscordSettings
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string BotAdminRole { get; set; }
    }    
    public class CachingSettings
    {        
        public int ModuleCachingInMinutes { get; set; }
    }
}
