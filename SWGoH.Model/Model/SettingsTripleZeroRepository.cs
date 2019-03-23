using SWGoH.Model.Model;
using SWGoH.Model.Model.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model.Settings.TripleZeroRepository
{
    public class SettingsTripleZeroRepository
    {
        public MongoDBSettings MongoDBSettings { get; set; }
        public RepoCachingSettings CachingSettings { get; set; }
        public SWGoHHelpSettings SWGoHHelpSettings { get; set; }
        public DiagnosticSettings DiagnosticSettings { get; set; }
    }    
    public class RepoCachingSettings
    {
        public short RepositoryCachingInMinutes { get; set; }
    }

    public class DiagnosticSettings
    {
        public bool ConsolePerformanceWatcher { get; set; }
    }
}
