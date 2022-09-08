using System.ComponentModel;
using System.Text.Json;

namespace Eskisehirspor.Application.Common.Extensions
{
    public static class JsonExtenstions
    {
        public static string ToJSON(this object serializedObject)
        {
            JsonSerializerOptions serializerSettings = new JsonSerializerOptions();
            //serializerSettings.Converters.Add(new StringConverter());
            return JsonSerializer.Serialize(serializedObject, serializerSettings);
        }

        public static string ToJSON(this object serializedObject, JsonSerializerOptions serializerSettings)
        {
            return JsonSerializer.Serialize(serializedObject, serializerSettings);
        }

        public static T DeserializeJSON<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public static object DeserializeJSON(this string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type);
        }

    }
}
