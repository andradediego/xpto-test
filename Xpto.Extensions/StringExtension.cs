using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xpto
{
    public static class StringExtension
    {
        public static string RemoverCaracteresInvalidos(this string strIn)
        {   
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }            
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
