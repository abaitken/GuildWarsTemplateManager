using System.Reflection;
using TemplateManager.Common;

namespace TemplateManager.Services
{
    internal class ApplicationInformation : ApplicationInformationBase
    {
        public ApplicationInformation()
            : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}