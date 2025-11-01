using Newtonsoft.Json;
using System.Xml;

namespace Json
{
    public static class JsonCfg
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string ToJson(object obj) => JsonConvert.SerializeObject(obj, Settings);
    }
}
