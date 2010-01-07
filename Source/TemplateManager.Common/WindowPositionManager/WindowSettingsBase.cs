using System.Configuration;
using System.Windows;

namespace TemplateManager.Common.WindowPositionManager
{
    public abstract class WindowSettingsBase : ApplicationSettingsBase, IWindowSettings
    {
        #region IWindowSettings Members

        [UserScopedSetting]
        public Rect Location
        {
            get
            {
                if(this["Location"] != null)
                    return (Rect) this["Location"];

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