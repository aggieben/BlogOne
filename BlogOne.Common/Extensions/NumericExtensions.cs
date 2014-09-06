using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BlogOne.Common.Extensions
{
    public static class NumericExtensions
    {
        private const string Base62Lookup = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBase62(this long n)
        {
            var sb = new StringBuilder();

            while (n > 0)
            {
                var m = (int) (n%62);
                sb.Insert(0, Base62Lookup[m]);
                n = (n - m)/62;
            }

            return sb.ToString();
        }

        public static string ToBase62(this int n)
        {
            return ToBase62((long) n);
        }

        public static long FromBase62(this string s)
        {
            long val = 0;
            var place = 1;
            
            foreach (var c in s.Reverse())
            {
                val += Base62Lookup.IndexOf(c)*place;
                place *= 62;
            }

            return val;
        }    
    }
}
