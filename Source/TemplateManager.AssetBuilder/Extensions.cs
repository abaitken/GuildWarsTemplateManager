using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using TemplateManager.BitmapEncoding;
using TemplateManager.Services;
using Attribute=TemplateManager.Services.Attribute;
using BitmapEncoder=TemplateManager.BitmapEncoding.BitmapEncoder;

namespace TemplateManager
{
    internal static class Extensions
    {
        public static void ForEach(this StringCollection source, Action<string> action)
        {
            foreach(var item in source)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(var item in source)
                action(item);
        }

        public static void AddImages(this ResourceWriter source, IDictionary<string, BitmapImage> images, EncoderType encoderType)
        {
            foreach(var image in images)
            {
                var encoder = BitmapEncoder.CreateBitmapEncoder(encoderType);
                source.AddResource(image.Key.ToLower(), encoder.GetImageSource(image.Value));
            }
        }

        public static IDictionary<string, BitmapImage> DownloadImages(this IEnumerable<Skill> source)
        {
            return source.DownloadImages(i => i.Image);
        }

        public static IDictionary<string, BitmapImage> DownloadImages(this IEnumerable<Profession> source)
        {
            return source.DownloadImages(i => i.Image);
        }

        private static IDictionary<string, BitmapImage> DownloadImages<T>(this IEnumerable<T> source,
                                                                          Func<T, string> imageProperty)
        {
            var client = new WebClient();

            var result = new Dictionary<string, BitmapImage>();

            foreach(var item in source)
            {
                if(result.ContainsKey(imageProperty(item)))
                    continue;

                var url = string.Format("http://localhost/TemplateManagerSkillDataService/uploads/{0}",
                                        imageProperty(item));

                var data = client.DownloadData(url);
                var stream = new MemoryStream(data);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                result.Add(imageProperty(item), image);
            }

            return result;
        }

        public static string CreateNativeProfessionXaml(this Profession source)
        {
            var attributes = new Dictionary<string, object>
                                 {
                                     {
                                         "Image",
                                         CreateImageReference(source.Image, "png")
                                         },
                                     {
                                         "Name", source.Name
                                         },
                                     {
                                         "Id", source.Id
                                         },
                                     {
                                         "ProfessionId", source.ProfessionId
                                         },
                                     {
                                         "ValidPrimary", source.ValidPrimary
                                         },
                                     {
                                         "ValidSecondary", source.ValidSecondary
                                         },
                                 };
            return CreateNativeDataXaml(attributes, "NativeProfession");
        }



        public static string CreateNativeAttributeXaml(this Attribute source)
        {
            var attributes = new Dictionary<string, object>
                                 {
                                     {"Id", source.Id},
                                     {"AttributeId", source.AttributeId},
                                     {"Name", source.Name},
                                     {"Profession", source.Profession},
                                     {"IsPrimary", source.IsPrimary}
                                 };
            return CreateNativeDataXaml(attributes, "NativeSkillAttribute");
        }



        public static string CreateNativeSkillTypeXaml(this SkillType source)
        {
            var attributes = new Dictionary<string, object>
                                 {
                                     {"Id", source.Id},
                                     {"Name", source.Name},
                                 };
            return CreateNativeDataXaml(attributes, "NativeSkillType");
        }



        public static string CreateNativeCampaignXaml(this Campaign source)
        {
            var attributes = new Dictionary<string, object>
                                 {
                                     {"Id", source.Id},
                                     {"Name", source.Name},
                                 };
            return CreateNativeDataXaml(attributes, "NativeCampaign");
        }

        public static string CreateNativeSkillXaml(this Skill source)
        {
            var attributes = new Dictionary<string, object>
                                 {
                                     {
                                         "Image",
                                         CreateImageReference(source.Image, "jpg")
                                         },
                                     {
                                         "AdrenalineCost", source.AdrenalineCost
                                         },
                                     {
                                         "Attribute", source.Attribute
                                         },
                                     {
                                         "Campaign", source.Campaign
                                         },
                                     {
                                         "CastingTime", source.CastingTime
                                         },
                                     {
                                         "Description", Escape(source.Description)
                                         },
                                     {
                                         "EnergyCost", source.EnergyCost
                                         },
                                     {
                                         "EnergyRegen", source.EnergyRegen
                                         },
                                     {
                                         "HealthSacrifice", source.HealthSacrifice
                                         },
                                     {
                                         "Id", source.Id
                                         },
                                     {
                                         "IsElite", source.IsElite
                                         },
                                     {
                                         "MonsterOnly", source.MonsterOnly
                                         },
                                     {
                                         "Name", Escape(source.Name)
                                         },
                                     {
                                         "Profession", source.Profession
                                         },
                                     {
                                         "PveOnly", source.PveOnly
                                         },
                                     {
                                         "PvpVersion", source.PvpVersion
                                         },
                                     {
                                         "RechargeTime", source.RechargeTime
                                         },
                                     {
                                         "SkillId", source.SkillId
                                         },
                                     {
                                         "SkillType", source.SkillType
                                         },
                                     {
                                         "Tags", Escape(source.Tags)
                                         },
                                     {
                                         "WikiLink", Escape(source.WikiLink)
                                         },
                                 };
            return CreateNativeDataXaml(attributes, "NativeSkill");
        }

        private static string CreateImageReference(string source, string imageFormat)
        {
            return string.Format(@"{{y:StaticResource {0}}}",
                                 source.Replace(string.Format(".{0}", imageFormat), string.Empty));
        }

        private static string CreateNativeDataXaml(IEnumerable<KeyValuePair<string, object>> source, string elementName)
        {
            const string format =
                @"<{0} {1}/>";

            var attributeBuilder = new StringBuilder();

            foreach(var item in source)
                attributeBuilder.AppendFormat(@"{0}=""{1}"" ", item.Key, item.Value);

            return string.Format(format, elementName, attributeBuilder);
        }

        private static string Escape(string source)
        {
            if(string.IsNullOrEmpty(source))
                return string.Empty;

            var result = Regex.Replace(source, @"""", "&quot;");
            result = Regex.Replace(result, @"<", "&lt;");
            return Regex.Replace(result, @">", "&gt;");
        }
    }
}