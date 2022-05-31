using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Utils
{
    public static class Extensions
    {
        public static ulong ToULong(this string str)
        {
            try
            {
                return ulong.Parse(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
