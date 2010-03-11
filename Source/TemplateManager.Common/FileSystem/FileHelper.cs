using System;
using Microsoft.VisualBasic.FileIO;

namespace TemplateManager.Common.FileSystem
{
    public static class FileHelper
    {
        public static bool Delete(string path, bool recycle)
        {
            var option = recycle ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently;

            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path,
                                                                   UIOption.AllDialogs,
                                                                   option,
                                                                   UICancelOption.ThrowException);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}