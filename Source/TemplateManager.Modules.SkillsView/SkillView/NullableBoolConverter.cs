using System;
using System.Globalization;
using System.Windows.Data;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    public class NullableBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              CultureInfo culture)
        {
            var item = (bool?) value;

            return item.HasValue ? (object) item.Value : null;
        }


        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            bool? result = null;

            if(value != null)
                result = (bool) value;

            return result;
        }

        #endregion
    }
}