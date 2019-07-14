using System;

namespace Model
{
    public class PutCallCurrencyModel: BaseFinType, IValidable
    {
        private Currency _putCurrency;
        private Currency _callCurrency;
        public Currency putCurrency
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

        public Currency callCurrency
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