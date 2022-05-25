using System.Runtime.Serialization;
using Example.Common.Json;

namespace Example.Common;

[DataContract]
public class OrderDetail : IJsonable
{
    [DataMember(Order = 1)]
    public string Client { get; set; }
    [DataMember(Order = 2)]
    public string Item { get; set; }
    [DataMember(Order = 3)]
    public int Quantity { get; set; }
}