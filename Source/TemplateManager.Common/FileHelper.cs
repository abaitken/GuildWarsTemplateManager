using Microsoft.VisualBasic.FileIO;
using System;

namespace TemplateManager.Common
{
    public static class FileHelper
    {
        public static bool Delete(string path, bool recycle)
        {
            var option = recycle ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently;

            try
            {
                FileSystem.DeleteFile(path,
                                    UIOption.AllDialogs,
                                    option,
                                    UICancelOption.ThrowException);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
