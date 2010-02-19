using System;
using System.ComponentModel;
using System.Configuration;
using TemplateManager.Infrastructure;

namespace TemplateManager
{
    internal class ApplicationSettings : ApplicationSettingsBase, IApplicationSettings
    {
        private const string DeleteBehaviourSettingsName = "DeleteBehaviour";
        private const string ArchiveFolderSettingsName = "ArchiveFolder";
        private const string TemplateFolderSettingName = "TemplateFolder";


        #region IApplicationSettings Members

        [UserScopedSetting]
        public string TemplateFolder
        {
            get { return Get<string>(TemplateFolderSettingName, TemplateStoreLocator.FindBuildStorePath); }
            set { this[TemplateFolderSettingName] = value; }
        }

        [UserScopedSetting]
        public string ArchiveFolder
        {
            get { return Get(ArchiveFolderSettingsName, () => string.Empty); }
            set { this[ArchiveFolderSettingsName] = value; }
        }

        [UserScopedSetting]
        public string DeleteBehaviour
        {
            get { return Get(DeleteBehaviourSettingsName, () => "Delete and Recycle"); }
            set { this[DeleteBehaviourSettingsName] = value; }
        }

        public event EventHandler TemplateFolderChanged;
        public event EventHandler ArchiveFolderChanged;
        public event EventHandler DeleteBehaviourChanged;

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
                case ArchiveFolderSettingsName:
                    RaiseChangedEvent(sender, ArchiveFolderChanged);
                    break;
                case DeleteBehaviourSettingsName:
                    RaiseChangedEvent(sender, DeleteBehaviourChanged);
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