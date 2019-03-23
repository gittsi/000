using SWGoH.Model;
using SWGoH.Model.Settings.TripleZeroBot;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TripleZero.Bot.Settings;

namespace TripleZero.Bot.Helper
{
    public class Logo
    {
        SettingsTripleZeroBot _settingsTripleZeroBot;

        public Logo(ApplicationSettings applicationSettings)
        {
            _settingsTripleZeroBot = applicationSettings.GetTripleZeroBotSettings();
        }

        public async void ConsolePrintLogo() //prints application name,version etc
        {
            await Task.FromResult(1);
            //get application Settings
            //var appSettings = IResolver.Current.ApplicationSettings.GetTripleZeroBotSettings();

            Version version = Assembly.GetEntryAssembly().GetName().Version;
            Consoler.WriteLineInColor(string.Format("{0} - {1}", _settingsTripleZeroBot.GeneralSettings.ApplicationName, _settingsTripleZeroBot.GeneralSettings.Environment), ConsoleColor.DarkGreen);
            Consoler.WriteLineInColor(string.Format("Application Version : {0}", version), ConsoleColor.DarkGreen);
            //Consoler.WriteLineInColor(string.Format("Json Version : {0}", appSettings.GeneralSettings.JsonSettingsVersion), ConsoleColor.DarkYellow);
            Console.Title = string.Format("{0} - version {1}", _settingsTripleZeroBot.GeneralSettings.ApplicationName, version);
            Console.WriteLine(); Console.WriteLine();
        }

        public string GetLogo() //prints application name,version etc
        {
            //get application Settings
            //var appSettings = IResolver.Current.ApplicationSettings.GetTripleZeroBotSettings();

            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string retStr = string.Format("{0} - {1}", _settingsTripleZeroBot.GeneralSettings.ApplicationName, _settingsTripleZeroBot.GeneralSettings.Environment);
            retStr += string.Format("\nApplication Version : {0}", version);

            return retStr;
        }
    }
}
