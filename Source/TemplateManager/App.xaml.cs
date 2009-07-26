using System;
using System.Windows;
using ExceptionReporting;
using TemplateManager.Common;
using TemplateManager.Properties;

namespace TemplateManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static class Const
        {
            public static readonly CommandLineOption ResetOption = new CommandLineOption("reset", "r");
        }

        private static IThemeManager themeManager;

        public static IThemeManager ThemeManager { get { return themeManager; } }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
#endif

            var arguments = CommandLineParser.Parse(e.Args);

            if (arguments[Const.ResetOption])
                Settings.Default.Reset();

            themeManager = new ThemeManager();
            
            StartCal();
        }

        private static void StartCal()
        {
            var boostrapper = new Bootstrapper();
            boostrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Settings.Default.Save();
        }

        static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var reporter = new ExceptionReporter();
            reporter.Config.EmailReportAddress = "logaangarius@hotmail.com";
            reporter.Config.ShowButtonIcons = false;

            var exception = e.ExceptionObject as Exception;
            reporter.Show(exception);

            Environment.Exit(1);
        }
    }
}