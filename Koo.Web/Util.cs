using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koo.Web
{
    public static class Util
    {
        public static string RemoveLeft(string str, int count)
        {

            if (string.IsNullOrEmpty(str.Trim()) || str.Count() < count)
            {
                return string.Empty;
            }

            return (string)str.Substring(0, count);

        }
    }
}