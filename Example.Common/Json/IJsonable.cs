using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Example.Common.Json
{
    public interface IJsonable
    {
    }

    public static class JsonableExtensions
    {
        private static readonly JsonSerializerSettings CamelJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
            
        public static string AsJson(this IJsonable jsonable)
        {
            return JsonConvert.SerializeObject(jsonable, CamelJsonSerializerSettings);
        }
        
        public static T ConvertJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}