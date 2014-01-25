using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogOne.Web.Extensions
{
    public static class ObjectExtensions
    {
        public static bool HasValue(this object obj)
        {
            return obj != null;
        }
    }
}