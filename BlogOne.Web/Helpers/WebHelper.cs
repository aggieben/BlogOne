using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlogOne.Web.Helpers
{
    public class WebHelper
    {
        private static Dictionary<string, string> _cachedPathInputs;

        public static string GetRandomizedUriComponent(string input)
        {
            if (_cachedPathInputs.ContainsKey(input))
            {
                return _cachedPathInputs[input];
            }

            var needsGeneration = false;
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (c != '-')
	            {
		            if (needsGeneration)
		            {
			            for (var ci = (char)(sb[sb.Length-1]+1); ci <= c; ci = (char)(ci+1))
			            {
				            sb.Append(ci);
			            }
			            needsGeneration = false;
		            }
		            else
		            {
			            sb.Append(c);
		            }
	            }
	            else 
	            {
		            if (needsGeneration) throw new FormatException(String.Format("Unexpected '-'; [{0}]", sb));
		            needsGeneration = true;
	            }
            }
            return _cachedPathInputs[input] = sb.ToString();            
        }
    }
}