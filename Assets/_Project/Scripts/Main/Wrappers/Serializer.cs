using Newtonsoft.Json;

namespace Main.Wrappers
{
    public static class Serializer
    {
        public static string ToJson(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}