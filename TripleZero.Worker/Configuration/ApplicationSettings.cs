using Microsoft.Extensions.Configuration;
using SWGoH.Model.Model.Settings;
using SWGoH.Model.Settings.Worker;
using System;

namespace TripleZero.Worker.Settings
{
    public class ApplicationSettings
    {
        private readonly IConfigurationRoot _SettingsConfigurationRoot;
        public ApplicationSettings(ISettingsConfiguration settingsConfiguration)
        {
            _SettingsConfigurationRoot = settingsConfiguration.GetConfiguration();
        }
        public SettingsWorker GetSettingsWorker()
        {
            var boolRepositoryCachingInMinutes = int.TryParse(_SettingsConfigurationRoot.GetSection("Caching_Settings")["RepositoryCachingInMinutes"], out int repositoryCachingInMinutes);
            var boolTokenCachingInMinutes = short.TryParse(_SettingsConfigurationRoot.GetSection("SWGoHHelp_Settings")["TokenCachingInMinutes"], out short TokenCachingInMinutes);

            SettingsWorker appSettings = new SettingsWorker
            {
                //general settings
                GeneralSettings = new GeneralSettings()
                {
                    ApplicationName = _SettingsConfigurationRoot.GetSection("General_Settings")["ApplicationName"]
                    ,
                    Environment = _SettingsConfigurationRoot.GetSection("General_Settings")["Environment"]
                    ,
                    JsonSettingsVersion = _SettingsConfigurationRoot.GetSection("General_Settings")["JsonSettingsVersion"]                   
                },
                //mongoDB settings
                MongoDBSettings = new MongoDBSettings()
                {
                    ApiKey = _SettingsConfigurationRoot.GetSection("MongoDB_Settings")["ApiKey"]
                     ,
                    DB = _SettingsConfigurationRoot.GetSection("MongoDB_Settings")["DB"]
                     ,
                    ConnectionString = _SettingsConfigurationRoot.GetSection("MongoDB_Settings")["ConnectionString"]

                },
                //SWGoHHelp settings
                SWGoHHelpSettings = new SWGoHHelpSettings()
                {
                    UserName = _SettingsConfigurationRoot.GetSection("SWGoHHelp_Settings")["UserName"]
                    ,
                    Password = _SettingsConfigurationRoot.GetSection("SWGoHHelp_Settings")["Password"]
                    ,
                    TokenCachingInMinutes = TokenCachingInMinutes
                }               
            };
            return appSettings;
        }       
    }
}
