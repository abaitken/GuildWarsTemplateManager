using Prism.Commands;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using TemplateManager.Common;

namespace TemplateManager.Modules.Workspace.Presentation.About
{
    internal class AboutViewModel : ViewModelBase, IAboutViewModel
    {
        private const string guildWarsCopyRight =
            @"Guild Wars is a trademark of NCsoft Corporation. Copyright © NCsoft Corporation. All rights reserved.
© 2004 ArenaNet, Inc. All content of this application is copyright ArenaNet, a wholly-owned subsidiary of NCsoft Corporation.
All rights reserved. ArenaNet, Arena.net and the ArenaNet logo, as well as Guild Wars, are trademarks or registered trademarks of NCsoft Corporation.";

        private readonly IApplicationInformationService applicationInformationService;
        private readonly IAboutView view;

        public AboutViewModel(IAboutView view, IApplicationInformationService applicationInformationService)
        {
            this.view = view;
            this.applicationInformationService = applicationInformationService;
            view.Model = this;

            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
        }

        public string GuildWarsCopyright
        {
            get { return guildWarsCopyRight; }
        }

        public string ProductName
        {
            get { return applicationInformationService.AssemblyProduct; }
        }

        #region IAboutViewModel Members

        public string AssemblyCopyright
        {
            get { return applicationInformationService.AssemblyCopyright; }
        }

        public string Title
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                                     "About {0}",
                                     applicationInformationService.AssemblyTitle);
            }
        }

        public string ProductAndVersion
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                                     "{0} v{1} ({2})",
                                     applicationInformationService.AssemblyProduct,
                                     applicationInformationService.FileVersion,
                                     applicationInformationService.AssemblyConfiguration);
            }
        }


        public ICommand CloseWindowCommand { get; private set; }

        public IAboutView View
        {
            get { return view; }
        }

        public IEnumerable<string> Credits
        {
            get
            {
                yield return "Alex Boyne-Aitken, Lead Programmer";
                //yield return "Matt (RavenIII), Artwork";
            }
        }

        #endregion

        private static void OnCloseWindow(Window obj)
        {
            obj.Close();
        }
    }
}