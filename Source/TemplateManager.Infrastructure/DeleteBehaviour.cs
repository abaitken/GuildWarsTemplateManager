using System.Collections.Generic;

namespace TemplateManager.Infrastructure
{
    public static class DeleteBehaviour
    {
        public const string MoveAndArchive = "Move and Archive";
        public const string DeleteAndRecycle = "Delete and Recycle";
        public const string DeletePermanently = "Delete permanently";

        public static IEnumerable<string> Values
        {
            get
            {
                yield return MoveAndArchive;
                yield return DeleteAndRecycle;
                yield return DeletePermanently;
            }
        }
    }
}