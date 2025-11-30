using System.Text.Json.Serialization;

namespace Ordering.Domain.AggregatesModels.OrderAggregate
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        New = 0,
        InProgress = 1,
        Delivering = 2,
        Compled = 3,
    }
}
