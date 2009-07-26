using System;
using System.Windows.Threading;

namespace TemplateManager.Common.ViewModel
{
    public abstract class LiveDataModelBase : DataModelBase
    {
        private readonly DispatcherTimer timer;

        protected LiveDataModelBase(TimeSpan interval)
        {
            timer = new DispatcherTimer(DispatcherPriority.Background)
                        {
                            Interval = interval
                        };
            timer.Tick += delegate { ScheduleUpdate(); };
        }

        protected override void OnActivated()
        {
            VerifyCalledOnUIThread();

            base.OnActivated();

            timer.Start();

            ScheduleUpdate();
        }

        protected override void OnDeactivated()
        {
            VerifyCalledOnUIThread();

            base.OnDeactivated();

            timer.Stop();
        }

        protected abstract void ScheduleUpdate();
    }
}