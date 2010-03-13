using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TemplateManager.Common.ViewModel
{
    public abstract class BackgroundLoadingViewModel : ViewModelBase
    {
        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if(isLoading == value)
                    return;

                isLoading = value;
                SendPropertyChanged("IsLoading");
            }
        }
        private bool viewLoaded;

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
            worker.RunWorkerAsync();
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