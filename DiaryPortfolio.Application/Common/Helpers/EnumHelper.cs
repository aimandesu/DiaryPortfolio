using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Common.Helpers
{
    public static class EnumHelper
    {
        public static bool TryParseEnum<T>(string value, out T result, bool ignoreCase = true)
            where T : struct, Enum
        {
            return Enum.TryParse(value, ignoreCase, out result);
        }

        public static T ParseEnumOrThrow<T>(string value, bool ignoreCase = true)
            where T : struct, Enum
        {
            if (Enum.TryParse(value, ignoreCase, out T result))
                return result;

            throw new ArgumentException($"Invalid value '{value}' for enum {typeof(T).Name}");
        }
    }
}
