using System.Configuration;
using System.Windows;

namespace TemplateManager.WindowPositionManager
{
    internal class WindowApplicationSettings : ApplicationSettingsBase, IWindowApplicationSettings
    {
        // TODO : This only permits one instance of these settings

        #region IWindowApplicationSettings Members

        [UserScopedSetting]
        public Rect Location
        {
            get
            {
                if(this["Location"] != null)
                    return ((Rect) this["Location"]);

                return Rect.Empty;
            }
            set { this["Location"] = value; }
        }

        [UserScopedSetting]
        public WindowState WindowState
        {
            get
            {
                if(this["WindowState"] != null)
                    return (WindowState) this["WindowState"];

                return WindowState.Normal;
            }
            set { this["WindowState"] = value; }
        }

        #endregion
    }
}