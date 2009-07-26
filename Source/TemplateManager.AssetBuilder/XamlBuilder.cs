using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using TemplateManager.Services;
using Attribute=TemplateManager.Services.Attribute;

namespace TemplateManager
{
    internal static class XamlBuilder
    {
        public static StringBuilder GetXaml(DataFetcher data)
        {
            var xaml = new StringBuilder();

            xaml.AppendLine(
                @"<NativeData xmlns=""clr-namespace:TemplateManager.Assets;assembly=TemplateManager.Assets"" 
              xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" 
              xmlns:y=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");


            xaml.AppendLine(WrapInElement("NativeData.Resources", CreateImageResources(data.SkillImages, data.ProfessionImages)));
            xaml.AppendLine(WrapInElement("NativeData.Professions", CreateProfessions(data.Professions)));
            xaml.AppendLine(WrapInElement("NativeData.Attributes", CreateAttributes(data.Attributes)));
            xaml.AppendLine(WrapInElement("NativeData.Skills", CreateSkills(data.Skills)));
            xaml.AppendLine(WrapInElement("NativeData.SkillTypes", CreateSkillTypes(data.SkillTypes)));
            xaml.AppendLine(WrapInElement("NativeData.Campaigns", CreateCampaigns(data.Campaigns)));

            xaml.AppendLine("</NativeData>");

            return xaml;
        }

        internal static string CreateCampaigns(IEnumerable<Campaign> campaigns)
        {
            Console.WriteLine("Building campaign data items");

            var xaml = new StringBuilder();

            foreach(var item in campaigns)
                xaml.AppendLine(item.CreateNativeCampaignXaml());

            return xaml.ToString();
        }

        internal static string CreateSkillTypes(IEnumerable<SkillType> skillTypes)
        {
            Console.WriteLine("Building skill type data items");

            var xaml = new StringBuilder();

            foreach(var item in skillTypes)
                xaml.AppendLine(item.CreateNativeSkillTypeXaml());

            return xaml.ToString();
        }

        private static string WrapInElement(string elementName, string contents)
        {
            return string.Format(
                @"<{0}>
{1}
</{0}>"
                ,
                elementName,
                contents);
        }

        internal static string CreateSkills(IEnumerable<Skill> skills)
        {
            Console.WriteLine("Building skill data items");

            var xaml = new StringBuilder();

            foreach(var item in skills)
                xaml.AppendLine(item.CreateNativeSkillXaml());

            return xaml.ToString();
        }

        internal static string CreateAttributes(IEnumerable<Attribute> attributes)
        {
            Console.WriteLine("Building attribute data items");

            var xaml = new StringBuilder();

            foreach(var item in attributes)
                xaml.AppendLine(item.CreateNativeAttributeXaml());

            return xaml.ToString();
        }

        internal static string CreateProfessions(IEnumerable<Profession> professions)
        {
            Console.WriteLine("Building profession data items");

            var xaml = new StringBuilder();

            foreach(var item in professions)
                xaml.AppendLine(item.CreateNativeProfessionXaml());

            return xaml.ToString();
        }

        internal static string CreateImageResources(IEnumerable<KeyValuePair<string, BitmapImage>> skillImages,
                                                   IEnumerable<KeyValuePair<string, BitmapImage>> professionImages)
        {
            Console.WriteLine("Building image resources");

            var xaml = new StringBuilder();

            foreach(var item in skillImages)
                xaml.AppendLine(CreateImageXaml(item));

            foreach(var item in professionImages)
                xaml.AppendLine(CreateImageXaml(item));

            return xaml.ToString();
        }

        internal static string CreateImageXaml(KeyValuePair<string, BitmapImage> source)
        {
            const string imageResourceFormat =
                @"<y:BitmapImage UriSource=""pack://application:,,,/TemplateManager.Assets;component/{0}"" x:Key=""{1}"" />";
            return string.Format(imageResourceFormat, source.Key, Regex.Replace(source.Key, @"\..{3,4}$", string.Empty ));
        }
    }
}