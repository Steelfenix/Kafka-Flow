namespace Kafka_Flow.ServiceCollectionExtensions.GenericsExtension;

public static class ByteArrayExtension
{
    public static string GetStringFromByteArray(this object byteArray, string defaultValue = "")
    {
        return byteArray is not byte[] rawMessage 
            ? defaultValue 
            : System.Text.Encoding.UTF8.GetString(rawMessage);
    }
}