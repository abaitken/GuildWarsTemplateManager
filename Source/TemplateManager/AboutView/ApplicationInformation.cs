using System;
using System.IO;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace TemplateManager.AboutView
{
    internal class ApplicationInformation : IApplicationInformationService
    {
        private readonly Assembly currentAssembly;

        [InjectionConstructor]
        public ApplicationInformation()
            : this(Assembly.GetExecutingAssembly())
        {
        }

        public ApplicationInformation(Assembly currentAssembly)
        {
            this.currentAssembly = currentAssembly;
        }

        #region IApplicationInformationService Members

        public string AssemblyTitle
        {
            get
            {
                var result = GetCustomAttribute<AssemblyTitleAttribute>(i => i.Title);

                if(string.IsNullOrEmpty(result))
                    return Path.GetFileNameWithoutExtension(currentAssembly.CodeBase);

                return result;
            }
        }

        public string AssemblyVersion
        {
            get { return currentAssembly.GetName().Version.ToString(); }
        }

        public string FileVersion
        {
            get { return GetCustomAttribute<AssemblyFileVersionAttribute>(i => i.Version); }
        }

        public string AssemblyConfiguration
        {
            get { return GetCustomAttribute<AssemblyConfigurationAttribute>(i => i.Configuration); }
        }

        public string AssemblyDescription
        {
            get { return GetCustomAttribute<AssemblyDescriptionAttribute>(i => i.Description); }
        }

        public string AssemblyProduct
        {
            get { return GetCustomAttribute<AssemblyProductAttribute>(i => i.Product); }
        }

        public string AssemblyCopyright
        {
            get { return GetCustomAttribute<AssemblyCopyrightAttribute>(i => i.Copyright); }
        }

        public string AssemblyCompany
        {
            get { return GetCustomAttribute<AssemblyCompanyAttribute>(i => i.Company); }
        }

        #endregion

        private string GetCustomAttribute<T>(Func<T, string> result)
        {
            var attributes = currentAssembly.GetCustomAttributes(typeof(T), false);

            if(attributes.Length == 0)
                return string.Empty;

            return result.Invoke((T) attributes[0]);
        }
    }
}