using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace TemplateManager.Common.ViewModel
{
    /// <summary>
    /// Base class for data models. All public methods must be called on the UI thread only.
    /// </summary>
    public abstract class DataModelBase : INotifyPropertyChanged
    {
        #region fields

        private readonly Dispatcher dispatcher;
        private bool isActive;
        private PropertyChangedEventHandler propertyChangedEvent;
        private ModelState state;

        #endregion

        #region contructors

        protected DataModelBase()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            State = ModelState.Invalid;
        }

        #endregion

        #region ModelState enum

        /// <summary>
        /// Possible states for a DataModel.
        /// </summary>
        public enum ModelState
        {
            Invalid, // The model is in an invalid state
            Fetching, // The model is being fetched
            Valid // The model has fetched its data
        }

        #endregion

        /// <summary>
        /// Is the model active?
        /// </summary>
        public bool IsActive
        {
            get
            {
                VerifyCalledOnUIThread();
                return isActive;
            }

            private set
            {
                VerifyCalledOnUIThread();
                if (value != isActive)
                {
                    isActive = value;
                    SendPropertyChanged("IsActive");
                }
            }
        }

        /// <summary>
        /// Gets or sets current state of the model.
        /// </summary>
        public ModelState State
        {
            get
            {
                VerifyCalledOnUIThread();
                return state;
            }

            set
            {
                VerifyCalledOnUIThread();
                if (value != state)
                {
                    state = value;
                    SendPropertyChanged("State");
                }
            }
        }

        /// <summary>
        /// The Dispatcher associated with the model.
        /// </summary>
        public Dispatcher Dispatcher
        {
            get { return dispatcher; }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// PropertyChanged event for INotifyPropertyChanged implementation.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                VerifyCalledOnUIThread();
                propertyChangedEvent += value;
            }
            remove
            {
                VerifyCalledOnUIThread();
                propertyChangedEvent -= value;
            }
        }

        #endregion

        /// <summary>
        /// Activate the model.
        /// </summary>
        public void Activate()
        {
            VerifyCalledOnUIThread();

            if (!isActive)
            {
                IsActive = true;
                OnActivated();
            }
        }

        /// <summary>
        /// Override to provide behavior on activate.
        /// </summary>
        protected virtual void OnActivated()
        {
        }

        /// <summary>
        /// Deactivate the model.
        /// </summary>
        public void Deactivate()
        {
            VerifyCalledOnUIThread();

            if (isActive)
            {
                IsActive = false;
                OnDeactivated();
            }
        }

        /// <summary>
        /// Override to provide behavior on deactivate.
        /// </summary>
        protected virtual void OnDeactivated()
        {
        }

        /// <summary>
        /// Utility function for use by subclasses to notify that a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void SendPropertyChanged(string propertyName)
        {
            VerifyCalledOnUIThread();
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Debugging utility to make sure functions are called on the UI thread.
        /// </summary>
        [Conditional("Debug")]
        protected void VerifyCalledOnUIThread()
        {
            Debug.Assert(Dispatcher.CurrentDispatcher == Dispatcher, "Call must be made on UI thread.");
        }
    }
}