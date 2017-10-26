using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ratul.Utility.Extensions
{
    public static class StringExtension
    {
        public static string RemoveAllHtmlTag(this String src)
        {
            return Regex.Replace(src, @"<[^>]+>", "");
        }
        public static string HtmlSpaceCharacterEntityToSpace(this String src)
        {
            return Regex.Replace(src, @"&nbsp;", " ");
        }
        public static string ToSingleWhiteSpace(this String src)
        {
            return Regex.Replace(src, @"\s+", " ");
        }
    }
}
