using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BlogOne.Web.Extensions
{
    public static  class StringExtensions
    {
        public static string f(this string str, params object[] args)
        {
            if (str.HasValue())
            {
                return String.Format(str, args);
            }

            return str;
        }    
    
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