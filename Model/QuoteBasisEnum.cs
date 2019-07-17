using System;
using System.Runtime.Serialization;

namespace Model
{
    public class QuoteBasisEnum: BaseFinType, IValidable
    {
        [DataMember(Name ="currency1PerCurrency2")]
        public float? Currency1PerCurrency2 { get; set; }
        [DataMember(Name ="currency2PerCurrency1")]
        public float? Currency2PerCurrency1 { get; set; }
        
        public void Validate()
        {
            CheckForNull();
            if (Currency1PerCurrency2 < 0 || Currency2PerCurrency1 < 0
                || Math.Abs((float)Currency1PerCurrency2 * (float)Currency2PerCurrency1 
                - 1) > epsilon)
            {
                throw new Exception("QuoteBasis is inconsistent");
            }
        }

        public float? GetRate()
        {
            return Currency2PerCurrency1;
        }
    }
}