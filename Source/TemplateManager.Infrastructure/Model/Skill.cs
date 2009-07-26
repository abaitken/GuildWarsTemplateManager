using System;
using System.Windows.Media.Imaging;

namespace TemplateManager.Infrastructure.Model
{
    public struct Skill
    {
        private readonly string description;
        private readonly BitmapImage image;
        private readonly string name;
        private readonly int nativeId;

        public Skill(int nativeId, string name, BitmapImage image, string description)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.nativeId = nativeId;
            this.name = name;
            this.image = image;
            this.description = description;
        }

        public int NativeId
        {
            get { return nativeId; }
        }

        public string Name
        {
            get { return name; }
        }

        public BitmapImage Image
        {
            get { return image; }
        }

        public string Description
        {
            get { return description; }
        }

        public override string ToString()
        {
            return string.Format("{0}", name);
        }
    }
}