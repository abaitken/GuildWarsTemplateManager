using System;

namespace TemplateManager.Common
{
    public class ViewDetails
    {
        private readonly string category;
        private readonly Uri imageUri;
        private readonly string name;

        public ViewDetails(string name, string category)
            : this(name, category, null)
        {
        }

        public ViewDetails(string name, string category, Uri imageUri)
        {
            this.imageUri = imageUri;
            this.name = name;
            this.category = category;
        }

        public string Category
        {
            get { return category; }
        }

        public string Name
        {
            get { return name; }
        }

        public Uri ImageUri
        {
            get { return imageUri; }
        }
    }
}
