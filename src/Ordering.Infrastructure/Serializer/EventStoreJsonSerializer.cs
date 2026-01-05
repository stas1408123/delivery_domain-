using System.Text.Json;

namespace Ordering.Infrastructure.Serializer
{
    public class EventStoreJsonSerializer : IEventStoreSerializer
    {
        private readonly JsonSerializerOptions _options;

        public EventStoreJsonSerializer()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = false,
            };
        }

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, _options);
        }
    }
}


