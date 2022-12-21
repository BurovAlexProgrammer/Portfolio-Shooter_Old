using System;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static string[] SplitLines(this string text)
        {
            return text.Split(Environment.NewLine);
        }
    }
}