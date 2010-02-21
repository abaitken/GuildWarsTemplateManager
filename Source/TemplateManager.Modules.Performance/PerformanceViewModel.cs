using System;
using System.Windows.Threading;
using TemplateManager.Common.ViewModel;

namespace TemplateManager.Modules.Performance
{
    internal class PerformanceViewModel : ViewModelBase, IPerformanceViewModel
    {
        private readonly IPerformanceView view;

        public PerformanceViewModel(IPerformanceView view)
        {
            this.view = view;
            view.Model = this;

            CreateTimer();
        }

        #region IPerformanceViewModel Members

        public IPerformanceView View
        {
            get { return view; }
        }

        public string MemoryUsage
        {
            get { return string.Format("{0:0.00} MB", GC.GetTotalMemory(false) / 1024.0 / 1024.0); }
        }

        public string HeaderText
        {
            get { return "Performance"; }
        }

        #endregion

        private void CreateTimer()
        {
            var timer = new DispatcherTimer
                            {
                                Interval = TimeSpan.FromSeconds(1)
                            };
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            SendPropertyChanged("MemoryUsage");
            SendPropertyChanged("ImageCacheItemsCount");
        }
    }
}