using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BuildManager.Infrastructure.Model
{
    public static class ImageCache
    {
        public static IEnumerable<string> ItemsInCache { get { return imageCache.Keys; } }
        private static readonly IDictionary<string, WeakReference> imageCache = new Dictionary<string, WeakReference>();

        public static ImageSource LoadImage(string imageUri)
        {
            WeakReference resultReference;

            if (imageCache.TryGetValue(imageUri, out resultReference))
            {
                if(resultReference.Target != null)
                    return resultReference.Target as ImageSource;
            }



            var result = new BitmapImage(new Uri(imageUri));

            if(resultReference == null)
                imageCache.Add(imageUri, new WeakReference(result));
            else
                imageCache[imageUri] = new WeakReference(result);

            return result;
        }
    }
}