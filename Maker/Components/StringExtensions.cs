using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    internal static class StringExtensions
    {
        public static string UCFirst(this string input)
            => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1));

        public static string LCFirst(this string input)
            => string.Concat(input[0].ToString().ToLower(), input.AsSpan(1));
    }
}
