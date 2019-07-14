namespace Model
{
    public interface IContract
    {
        int? id { get; set; }
        BuyerSellerModel buyerSellerModel { get; set; }
        PutCallCurrencyModel putCallCurrencyModel { get; set; }

        ExecutionPeriodDates executionPeriodDates { get; set; }
        
        Currency currency { get; }

        float? remainingAmount { get; set; }
    }
}