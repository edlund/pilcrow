using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Pilcrow.Core.Helpers
{
    public static class StringHelper
    {
        public static string Slugify(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            var normalised = value.Normalize(NormalizationForm.FormD).ToCharArray();
            value = new string(normalised.Where(c => (int)c <= 127).ToArray());
            value = value.ToLower();
            value = Regex.Replace(value, @"[^-a-zA-Z0-9\s]", "");
            value = Regex.Replace(value, @"\s+", " ");
            value = value.Trim();
            value = value.Replace(" ", "-");
            return value;
        }
        
        public static string Camelify(string value)
        {
            return ToTitleCase(string.Join(" ", Slugify(value).Split('-')), string.Empty);
        }
        
        public static string ToTitleCase(string value, string joiner = " ")
        {
            var parts = Regex.Split(value, @"\s+");
            return string.Join(joiner, from part in parts
                where !string.IsNullOrEmpty(part)
                select $"{part.Substring(0, 1).ToUpper()}{part.Substring(1)}"
            );
        }
    }
}

