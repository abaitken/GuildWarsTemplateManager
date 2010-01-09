using System;
using System.Windows;
using ExceptionReporting;

namespace TemplateManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
#endif

            var boostrapper = new Bootstrapper(e.Args);
            boostrapper.Run();
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