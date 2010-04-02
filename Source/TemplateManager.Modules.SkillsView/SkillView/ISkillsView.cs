﻿using TemperedSoftware.Shared.Presentation;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    public interface ISkillsView : IHeadedContent
    {
        ISkillsViewModel Model { get; set; }
    }
}