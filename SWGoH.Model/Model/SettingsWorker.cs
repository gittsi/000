using SWGoH.Model.Model.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model.Settings.Worker
{
    public class SettingsWorker
    {
        public GeneralSettings GeneralSettings { get; set; }
        public MongoDBSettings MongoDBSettings { get; set; }
        public SWGoHHelpSettings SWGoHHelpSettings { get; set; }
    }    
}
