using Microsoft.Extensions.Configuration;
using SWGoH.Model;

namespace TripleZero.Bot.Settings
{
    public class ApplicationSettings
    {
        private readonly IConfigurationRoot _SettingsConfigurationRoot;
        public ApplicationSettings(ISettingsConfiguration settingsConfiguration)
        {
            _SettingsConfigurationRoot = settingsConfiguration.GetConfiguration();
        }
        public SettingsTripleZeroBot GetTripleZeroBotSettings()
        {
            var boolModuleCachingInMinutes = int.TryParse(_SettingsConfigurationRoot.GetSection("Caching_Settings")["ModuleCachingInMinutes"], out int ModuleCachingInMinutes);

            SettingsTripleZeroBot appSettings = new SettingsTripleZeroBot
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
                //discord settings
                DiscordSettings = new DiscordSettings()
                {
                    Token = _SettingsConfigurationRoot.GetSection("Discord_Settings")["Token"]
                     ,
                    Prefix = _SettingsConfigurationRoot.GetSection("Discord_Settings")["Prefix"]
                     ,
                    BotAdminRole = _SettingsConfigurationRoot.GetSection("Discord_Settings")["BotAdminRole"]

                },
                CachingSettings = new CachingSettings()
                {
                    ModuleCachingInMinutes = ModuleCachingInMinutes
                }
            };
            return appSettings;
        }
        public SettingsTripleZeroRepository GetTripleZeroRepositorySettings()
        {
            var boolRepositoryCachingInMinutes = int.TryParse(_SettingsConfigurationRoot.GetSection("Caching_Settings")["RepositoryCachingInMinutes"], out int RepositoryCachingInMinutes);
            var boolTokenCachingInMinutes = short.TryParse(_SettingsConfigurationRoot.GetSection("SWGoHHelp_Settings")["TokenCachingInMinutes"], out short TokenCachingInMinutes);


            SettingsTripleZeroRepository appSettings = new SettingsTripleZeroRepository
            {               
                MongoDBSettings = new MongoDBSettings()
                {
                    ApiKey = _SettingsConfigurationRoot.GetSection("MongoDB_Settings")["ApiKey"]
                    ,
                    DB = _SettingsConfigurationRoot.GetSection("MongoDB_Settings")["DB"]
                    ,
                    ConnectionString = _SettingsConfigurationRoot.GetSection("MongoDB_Settings")["ConnectionString"]
                },

                SWGoHHelpSettings = new SWGoHHelpSettings()
                {
                    UserName = _SettingsConfigurationRoot.GetSection("SWGoHHelp_Settings")["UserName"]
                    ,
                    Password = _SettingsConfigurationRoot.GetSection("SWGoHHelp_Settings")["Password"]
                    ,
                    TokenCachingInMinutes = TokenCachingInMinutes
                },

                CachingSettings = new RepoCachingSettings()
                {                    
                    RepositoryCachingInMinutes = RepositoryCachingInMinutes
                }
            };
            return appSettings;
        }
    }
}
