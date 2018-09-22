using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TripleZero.Bot.Helper
{
    public class Logo
    {
        public async static void ConsolePrintLogo() //prints application name,version etc
        {
            //await Task.FromResult(1);
            ////get application Settings
            //var appSettings = IResolver.Current.ApplicationSettings.GetTripleZeroBotSettings();

            //Version version = Assembly.GetEntryAssembly().GetName().Version;
            //Consoler.WriteLineInColor(string.Format("{0} - {1}", appSettings.GeneralSettings.ApplicationName, appSettings.GeneralSettings.Environment), ConsoleColor.DarkGreen);
            //Consoler.WriteLineInColor(string.Format("Application Version : {0}", version), ConsoleColor.DarkGreen);
            ////Consoler.WriteLineInColor(string.Format("Json Version : {0}", appSettings.GeneralSettings.JsonSettingsVersion), ConsoleColor.DarkYellow);
            //Console.Title = string.Format("{0} - version {1}", appSettings.GeneralSettings.ApplicationName, version);
            //Console.WriteLine(); Console.WriteLine();
        }

        public static string GetLogo() //prints application name,version etc
        {
            ////get application Settings
            //var appSettings = IResolver.Current.ApplicationSettings.GetTripleZeroBotSettings();

            //Version version = Assembly.GetEntryAssembly().GetName().Version;
            //string retStr = string.Format("{0} - {1}", appSettings.GeneralSettings.ApplicationName, appSettings.GeneralSettings.Environment);
            //retStr+=string.Format("\nApplication Version : {0}", version);

            //return retStr;

            return "aaa";
        }
    }
}
