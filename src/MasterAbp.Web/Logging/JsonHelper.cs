using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MasterAbp.Web.Logging
{
    /// <summary>
    /// JsonHelper
    /// </summary>
    public class JsonHelper
    {
        private static JsonHelper _jsonHelper = new JsonHelper();
        public static JsonHelper Instance { get { return _jsonHelper; } }

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, GetOption());
        }

        public string SerializeByConverter(object obj, JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        public T Deserialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input);
        }

        public T DeserializeByConverter<T>(string input, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<T>(input, options);
        }

        public T DeserializeBySetting<T>(string input, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<T>(input, options);
        }

        private object NullToEmpty(object obj)
        {
            return null;
        }

        private static JsonSerializerOptions GetOption()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            return options;
        }
    }
}
