using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlogOne.Web.Helpers
{
    public class WebHelper
    {
        private static readonly Dictionary<string, string> _cachedPathInputs = new Dictionary<string, string>();

        public static string GetShortcode(int len, string input = "a-zA-Z0-9")
        {
            var expandedInput = ExpandShortcodeInput(input);
            var gen = new Random(DateTime.UtcNow.Millisecond);
            var sb = new StringBuilder(len);
            while (sb.Length < len)
            {
                var index = gen.Next(expandedInput.Length - 1);
                sb.Append(expandedInput[index]);
            }
            return sb.ToString();
        }

        private static string ExpandShortcodeInput(string input)
        {
            if (_cachedPathInputs.ContainsKey(input))
            {
                return _cachedPathInputs[input];
            }

            var needsGeneration = false;
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                if (c != '-')
                {
                    if (needsGeneration)
                    {
                        for (var ci = (char)(sb[sb.Length - 1] + 1); ci <= c; ci = (char)(ci + 1))
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