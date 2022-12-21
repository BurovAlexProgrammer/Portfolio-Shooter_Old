using System;

namespace _Project.Scripts.Main.Localizations
{
    public class LocalizationInfo: IEquatable<LocalizationInfo>
    {
        public string name;
        public string fullName;
        
        public LocalizationInfo() {}

        public LocalizationInfo(LocalizationInfo other)
        {
            if (other == null) return;
            name = other.name;
            fullName = other.fullName;
        }

        public bool Equals(LocalizationInfo other)
        {
            if (other == null) return false;
            return name == other.name && fullName == other.fullName;
        }
    }
}