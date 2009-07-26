
using System.Windows.Controls;

namespace TemplateManager.Modules.Performance
{
    /// <summary>
    /// Interaction logic for PerformanceView.xaml
    /// </summary>
    public partial class PerformanceView : UserControl, IPerformanceView
    {
        public PerformanceView()
        {
            InitializeComponent();
        }

        #region IPerformanceView Members

        public IPerformanceViewModel Model
        {
            get { return DataContext as IPerformanceViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}