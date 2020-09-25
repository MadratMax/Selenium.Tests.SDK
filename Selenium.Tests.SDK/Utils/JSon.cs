namespace Selenium.Tests.SDK.Utils
{
    using System;
    using Newtonsoft.Json;

    public class Json
    {
        public static T Deserialize<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value, (JsonSerializerSettings)null);
            }
            catch (JsonReaderException ex)
            {
                throw new Exception($"Deserialization failed with reader exception for type {value.GetType()}", ex);
            }
        }

        public static string Serialize(object obj)
        {
            var jsonData = JsonConvert.SerializeObject(obj);
            return jsonData;
        }
    }
}
