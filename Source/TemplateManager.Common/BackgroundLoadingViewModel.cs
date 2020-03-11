using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateManager.Common
{
    public abstract class BackgroundLoadingViewModel : ViewModelBase
    {
        private bool isLoading;

        private bool viewLoaded;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading == value)
                    return;

                isLoading = value;
                SendPropertyChanged("IsLoading");
            }
        }

        public void OnViewLoaded()
        {
            if (viewLoaded)
                return;

            viewLoaded = true;
            RunWorkerASync();

            OnViewLoadedImpl();
        }

        protected void RunWorkerASync()
        {
            IsLoading = true;

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompletedBase;
            worker.RunWorkerAsync(CreateThreadArgument());
        }

        protected virtual object CreateThreadArgument()
        {
            return null;
        }

        protected virtual void OnViewLoadedImpl()
        {
            // Do nothing
        }

        private void WorkerRunWorkerCompletedBase(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
            WorkerRunWorkerCompleted(sender, e);
        }

        protected abstract void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args);

        protected abstract void WorkerDoWork(object sender, DoWorkEventArgs e);
    }
}
