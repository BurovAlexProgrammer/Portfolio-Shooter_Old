using System;

namespace Main.Localizations
{
    public class LocalizationInfo: IEquatable<LocalizationInfo>
    {
        public string Name;
        public string FullName;
        
        public LocalizationInfo() {}

        public LocalizationInfo(LocalizationInfo other)
        {
            if (other == null) return;
            Name = other.Name;
            FullName = other.FullName;
        }

        public bool Equals(LocalizationInfo other)
        {
            if (other == null) return false;
            return Name == other.Name && FullName == other.FullName;
        }
    }
}