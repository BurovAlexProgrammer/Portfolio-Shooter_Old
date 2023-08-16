using System;
using System.Collections;
using System.Collections.Generic;

namespace Main.Extension
{
    public static partial class Common
    {
        public static bool CheckLabelledArray<T>(this T[] array, Type type)
        {
            var t = type;
            return true;//array.Length == type.
        }
    }
}