using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{
    internal class OpenFolderDialogCommand : CommandModelBase
    {
        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var helper = e.Parameter as IOpenFolderDialogHelper;

            if (helper == null)
                throw new InvalidOperationException("Expected parameter to be a dialog helper");

            var folder = helper.GetCurrentFolder();

            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
                folder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            var fd = new FolderBrowserDialog
                         {
                             SelectedPath = folder
                         };

            if (fd.ShowDialog() == DialogResult.OK)
                helper.SetNewFolder(fd.SelectedPath);
        }
    }
}