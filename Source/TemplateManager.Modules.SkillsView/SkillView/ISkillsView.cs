﻿using TemplateManager.Common;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    public interface ISkillsView : IHeadedContent
    {
        ISkillsViewModel Model { get; set; }
    }
}