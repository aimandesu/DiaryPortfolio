using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions _defaultOptions = new()
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };

        public static T? DeserializeForm<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, _defaultOptions);
        }
    }
}
