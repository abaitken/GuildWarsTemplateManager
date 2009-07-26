using System;
using System.Reflection;
using System.Windows.Markup;

namespace TemplateManager.Assets
{
    public class AssetLoader
    {
        public NativeData Load()
        {
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Const.XamlResourceName);

            if(resourceStream == null)
                throw new InvalidOperationException("Could not locate data resources");

            return XamlReader.Load(resourceStream) as NativeData;
        }
    }
}