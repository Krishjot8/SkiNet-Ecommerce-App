using System.Runtime.Serialization;

namespace ECommerce_App.Models.OrderAggregate
{
    public enum OrderStatus
    {

        [EnumMember(Value = "Pending")]

        Pending,


        [EnumMember(Value = "Pending")]


        PaymentRecieved,


        [EnumMember(Value = "Pending")]

        PaymentFailed
    }
}
