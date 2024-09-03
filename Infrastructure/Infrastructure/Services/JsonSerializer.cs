using Infrastructure.Services.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(string value)
        {
            var result = JsonConvert.DeserializeObject<T>(value);
            if (result == null)
            {
                throw new InvalidOperationException("Deserialization returned null");
            }

            return result;
        }
    }
}