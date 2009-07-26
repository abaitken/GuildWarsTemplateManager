using System;
using System.Windows.Media.Imaging;

namespace TemplateManager.Infrastructure.Model
{
    public class Profession : IComparable, IComparable<Profession>, IEquatable<Profession>
    {
        private readonly string name;
        private readonly BitmapImage image;
        private readonly int nativeId;
        private readonly bool validPrimary;
        private readonly bool validSecondary;

        public Profession(int nativeId, string name, BitmapImage image, bool validPrimary, bool validSecondary)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.nativeId = nativeId;
            this.name = name;
            this.image = image;
            this.validPrimary = validPrimary;
            this.validSecondary = validSecondary;
        }

        public int NativeId { get { return nativeId; } }

        public string Name { get { return name; } }
        public BitmapImage Image { get { return image; } }
        public bool ValidPrimary { get { return validPrimary; } }
        public bool ValidSecondary { get { return validSecondary; } }

        public override string ToString()
        {
            return string.Format("{0}", name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as Profession;

            if (other == null)
                return false;

            return Equals(other);
        }

        #region IEquatable<Profession> Members

        public bool Equals(Profession other)
        {
            return name.Equals(other.name);
        }

        #endregion

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var other = obj as Profession;

            if (other == null)
                return -1;

            return CompareTo(other);
        }

        #endregion

        #region IComparable<Profession> Members

        public int CompareTo(Profession other)
        {
            return Name.CompareTo(other.Name);
        }

        #endregion
    }
}