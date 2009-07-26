using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using Microsoft.CSharp;
using TemplateManager.Assets;
using TemplateManager.BitmapEncoding;

namespace TemplateManager
{
    class AssetBuilder
    {
        public static void CreateAssets()
        {
            var data = new DataFetcher(false);

            CreateXamlResource(data, Const.XamlResourceName);
            const string resourceName = "TemplateManager.Assets.g.resources";
            CreateImageResource(data, resourceName);

            var sourceFiles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Assets"),
                                                 "*.*",
                                                 SearchOption.AllDirectories);


            var provider = new CSharpCodeProvider(new Dictionary<String, String>
                                                      {
                                                          {
                                                              "CompilerVersion", "v3.5"
                                                              }
                                                      });

            Console.WriteLine("Compiling");

            const string outputAssembly = "TemplateManager.Assets.dll";

            const string keyFile = @"D:\Logaan\Documents\Visual Studio 2008\Projects\TemplateManager\Signing\TemplateManager.snk";
            var compilerParams = new CompilerParameters
            {
                OutputAssembly = outputAssembly,
                CompilerOptions = string.Format(@"/target:library /optimize ""/keyfile:{0}""", keyFile),
                GenerateExecutable = false,
                GenerateInMemory = false,
                IncludeDebugInformation = false,
            };

            compilerParams.EmbeddedResources.Add(resourceName);
            compilerParams.EmbeddedResources.Add(Const.XamlResourceName);
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("System.Xml.dll");
            compilerParams.ReferencedAssemblies.Add("WindowsBase.dll");
            compilerParams.ReferencedAssemblies.Add("PresentationCore.dll");
            compilerParams.ReferencedAssemblies.Add("PresentationFramework.dll");

            var results = provider.CompileAssemblyFromFile(compilerParams, sourceFiles);

            results.Output.ForEach(Console.WriteLine);

            const string outputFolder = @"D:\Logaan\Documents\Visual Studio 2008\Projects\TemplateManager\Libs\TemplateManager.Assets";
            var resultFileName = Path.Combine(outputFolder, outputAssembly);
            File.Copy(results.PathToAssembly, resultFileName, true);

            Console.WriteLine();
            Console.WriteLine("Done!");
        }

        private static void CreateXamlResource(DataFetcher data, string resourceName)
        {
            var xaml = XamlBuilder.GetXaml(data);
            File.WriteAllText(resourceName, xaml.ToString());
        }

        private static void CreateImageResource(DataFetcher data, string resourceName)
        {
            Console.WriteLine("Building image resource");
            var imageResources = new ResourceWriter(resourceName);

            imageResources.AddImages(data.SkillImages, EncoderType.Jpeg);
            imageResources.AddImages(data.ProfessionImages, EncoderType.Png);

            imageResources.Generate();
            imageResources.Close();
        }
    }
}
