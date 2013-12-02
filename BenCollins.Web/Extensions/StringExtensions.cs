using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenCollins.Web.Extensions
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
    }
}