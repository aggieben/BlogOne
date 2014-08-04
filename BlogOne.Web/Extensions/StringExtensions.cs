using BlogOne.Common.Extensions;
using System;
using System.Text.RegularExpressions;

namespace BlogOne.Web.Extensions
{
    public static class StringExtensions
    {
        public static string AsSlug(this string str)
        {
            if (!str.HasValue())
                return str;

            var tmp = Regex.Replace(str.Trim(), @"[^\w\s\-]", String.Empty).Replace(' ', '-');
            tmp = tmp.Substring(0, Math.Min(50, tmp.Length)).ToLower().TrimEnd('-');

            return tmp;
        }
    }
}