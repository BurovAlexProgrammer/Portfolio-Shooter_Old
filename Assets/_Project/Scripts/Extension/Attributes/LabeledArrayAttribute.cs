using System;
using UnityEngine;

namespace Main.Extension.Attributes
{
    public class LabeledArrayAttribute : PropertyAttribute
    {
        public readonly string[] names;

        public LabeledArrayAttribute(string[] names)
        {
            this.names = names;
        }

        public LabeledArrayAttribute(Type enumType)
        {
            names = Enum.GetNames(enumType);
        }
    }
}