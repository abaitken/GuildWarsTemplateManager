using System;
using System.Windows;
using System.Windows.Threading;
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
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
#endif
            var boostrapper = new Bootstrapper(e.Args);
            boostrapper.Run();
        }

        private static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var reporter = new ExceptionReporter();
            reporter.Config.EmailReportAddress = "logaangarius@hotmail.com";
            reporter.Config.ShowButtonIcons = false;

            reporter.Show(e.Exception);

            Environment.Exit(1);
        }
    }
}