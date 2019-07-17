//using Castle.Components.DictionaryAdapter.Xml;
using System;
using System.Runtime.Serialization;

namespace Model
{
    public class Currency: BaseFinType, IValidable, IEquatable<Currency>
    {
        [DataMember(Name ="currencyScheme")]
        public string CurrencyScheme{ get; set; }

        public bool Equals(Currency other)
        {
            return CurrencyScheme==other.CurrencyScheme;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CurrencyScheme))
            {
                throw new Exception($"currencyScheme of {this.GetType()} cannot be null or empty");
            }
        }
    }
}