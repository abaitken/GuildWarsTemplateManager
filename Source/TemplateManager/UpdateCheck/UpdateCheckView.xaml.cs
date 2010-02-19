using System;

namespace TemplateManager.UpdateCheck
{
    /// <summary>
    /// Interaction logic for UpdateCheck.xaml
    /// </summary>
    public partial class UpdateCheckView : IUpdateCheckView
    {
        public UpdateCheckView()
        {
            InitializeComponent();
        }

        public IUpdateCheckViewModel Model
        {
            get { return DataContext as IUpdateCheckViewModel; }
            set { DataContext = value; }
        }
    }
}
