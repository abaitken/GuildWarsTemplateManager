using System;
using System.ComponentModel;
using System.Configuration;
using TemplateManager.Infrastructure;

namespace TemplateManager
{
    internal class ApplicationSettings : ApplicationSettingsBase, IApplicationSettings
    {
        private const string TemplateFolderSettingName = "TemplateFolder";

        #region IApplicationSettings Members

        [UserScopedSetting]
        public string TemplateFolder
        {
            get { return Get<string>(TemplateFolderSettingName, TemplateStoreLocator.FindBuildStorePath); }
            set { this[TemplateFolderSettingName] = value; }
        }

        public event EventHandler TemplateFolderChanged;

        #endregion

        private T Get<T>(string settingName, Func<T> defaultValue)
        {
            if(this[settingName] != null)
                return (T) this[settingName];

            return defaultValue();
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case TemplateFolderSettingName:
                    RaiseChangedEvent(sender, TemplateFolderChanged);
                    break;
            }
        }

        private static void RaiseChangedEvent(object sender, EventHandler handler)
        {
            if(handler != null)
                handler(sender, new EventArgs());
        }
    }
}