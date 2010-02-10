using System.Collections.Generic;
using TemplateManager.DataFetcher.Model;

namespace TemplateManager.DataFetcher.DataTargets
{
    public interface IDataTarget
    {
        void Update(IEnumerable<Skill> data);
    }
}