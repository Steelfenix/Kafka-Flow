using Example.Common.Json;

namespace Example.Common;

public class OrderDetail : IJsonable
{
    public string Client { get; set; }
    public string Item { get; set; }
    public int Quantity { get; set; }
}