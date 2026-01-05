namespace Ordering.Infrastructure.Serializer
{
    public interface IEventStoreSerializer
    {
        string Serialize(object obj);
        object Deserialize(string json, Type type);
    }

}
