namespace Selenium.Tests.SDK.Settings.Selenium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class JsonDeserializer
    {
        public static Dictionary<string, object> GetDictionaryFromJObject(JObject obj)
        {
            var result = new Dictionary<string, object>();

            foreach (var property in obj)
            {
                result.Add(property.Key, GetTokenValue(property.Value));
            }

            return result;
        }

        private static object GetTokenValue(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return GetDictionaryFromJObject((JObject)token);

                case JTokenType.Array:
                    return GetArrayFromJArray((JArray)token);

                case JTokenType.String:
                    return token.ToObject<string>();

                case JTokenType.Boolean:
                    return token.ToObject<bool>();

                default:
                    return token.ToObject<string>();
            }
        }

        private static Array GetArrayFromJArray(JArray array)
        {
            return array.Select(GetTokenValue).ToArray();
        }
    }
}