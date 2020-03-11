using System.IO;

namespace TemplateManager.Common
{
    public static class FolderHelper
    {
        public static string CreatePathFromParts(string initialPart, params string[] parts)
        {
            var result = initialPart;

            foreach (var part in parts)
                result = Path.Combine(result, part);

            return result;
        }
    }
}
