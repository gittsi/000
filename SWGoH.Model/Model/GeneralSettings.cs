using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model.Model.Settings
{
    public class GeneralSettings
    {
        public string ApplicationName { get; set; }
        public string Environment { get; set; }
        public string JsonSettingsVersion { get; set; }
        public bool ConsolePerformanceWatcher { get; set; }
    }
}
