using System.Reflection;
using TemperedSoftware.Shared.Services;

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