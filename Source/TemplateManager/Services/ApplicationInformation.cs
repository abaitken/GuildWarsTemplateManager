using System.Reflection;
using InfiniteRain.Shared.Services;

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