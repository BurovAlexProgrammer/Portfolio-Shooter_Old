using System;

namespace Main.Extension
{
    public static partial class Common
    {
        public static string[] SplitLines(this string text)
        {
            return text.Split(Environment.NewLine);
        }
        
        public static string Format(this int value, StringFormat format)
        {
            switch (format)
            {
                case StringFormat.Time:
                    return TimeSpan.FromSeconds(value).ToString("hh':'mm':'ss");
                default:
                    return value.ToString();
            }
        }

        public enum StringFormat
        {
            Time
        }
    }
}