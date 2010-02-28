using System;

namespace TemplateManager.Infrastructure.Controllers
{
    public struct ViewDetails
    {
        private readonly string key;
        private readonly string name;
        private readonly Uri imageUri;
        
        public ViewDetails(string key, string name)
            : this(key, name, null)
        {
        }

        public ViewDetails(string key, string name, Uri imageUri)
        {
            this.key = key;
            this.imageUri = imageUri;
            this.name = name;
        }

        public string Key
        {
            get { return key; }
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