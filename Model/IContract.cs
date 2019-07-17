using System.Runtime.Serialization;

namespace Model
{
    public interface IContract
    {
        [DataMember(Name ="id")]
        int? Id { get; set; }
        BuyerSellerModel BuyerSellerModel { get; set; }
        PutCallCurrencyModel PutCallCurrencyModel { get; set; }

        ExecutionPeriodDates ExecutionPeriodDates { get; set; }
        [DataMember(Name ="currency")]
        Currency Currency { get; }

        float? RemainingAmount { get; set; }
    }
}