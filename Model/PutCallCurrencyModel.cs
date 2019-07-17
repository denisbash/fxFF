using System;
using System.Runtime.Serialization;

namespace Model
{
    public class PutCallCurrencyModel: BaseFinType, IValidable
    {
        private Currency _putCurrency;
        private Currency _callCurrency;
        [DataMember(Name ="putCurrency")]
        public Currency PutCurrency
        {
            get => _putCurrency;
            set
            {
                if (_putCurrency == null)
                {
                    _putCurrency = value;
                    ValidationEvent += _putCurrency.Validate;
                }
            }
        }
        [DataMember(Name ="callCurrency")]
        public Currency CallCurrency
        {
            get=>_callCurrency;
            set
            {
                if (_callCurrency == null)
                {
                    _callCurrency = value;
                    ValidationEvent += _callCurrency.Validate;
                }
            }
        }

        public void Validate()
        {
            CheckForNull();
            InvokeValidationEvent();
        }
    }
}