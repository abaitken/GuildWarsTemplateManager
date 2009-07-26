using System;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    class DeleteTemplateCommand : CommandModelBase
    {
        private readonly IServiceController service;

        public DeleteTemplateCommand(IServiceController service)
        {
            this.service = service;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var template = e.Parameter as SkillTemplate;

            if(template == null)
                throw new InvalidOperationException("Expected parameter to be a skill template");

            service.DeleteTemplate(template);
        }
    }
}