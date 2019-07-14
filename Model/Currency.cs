//using Castle.Components.DictionaryAdapter.Xml;
using System;

namespace Model
{
    public class Currency: BaseFinType, IValidable, IEquatable<Currency>
    {
        public string currencyScheme{ get; set; }

        public bool Equals(Currency other)
        {
            return currencyScheme==other.currencyScheme;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(currencyScheme))
            {
                throw new Exception($"currencyScheme of {this.GetType()} cannot be null or empty");
            }
        }
    }
}