using System;

namespace Model
{
    public class QuoteBasisEnum: BaseFinType, IValidable
    {
        public float? currency1PerCurrency2 { get; set; }
        public float? currency2PerCurrency1 { get; set; }
        
        public void Validate()
        {
            CheckForNull();
            if (currency1PerCurrency2 < 0 || currency2PerCurrency1 < 0
                || Math.Abs((float)currency1PerCurrency2 * (float)currency2PerCurrency1 
                - 1) > epsilon)
            {
                throw new Exception("QuoteBasis is inconsistent");
            }
        }

        public float? GetRate()
        {
            return currency2PerCurrency1;
        }
    }
}