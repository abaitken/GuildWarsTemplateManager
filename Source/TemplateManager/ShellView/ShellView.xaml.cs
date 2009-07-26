using System;
using System.Diagnostics;

namespace TemplateManager.ShellView
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : IShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public IShellViewModel Model
        {
            get { return DataContext as IShellViewModel; }
            set { DataContext = value; }
        }
    }
}