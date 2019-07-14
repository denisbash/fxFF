using System;

namespace Model
{
    public class Amount: BaseFinType, IValidable
    {
        private float? _amount;

        private Currency _currency;
        public Currency currency
        {
            get => _currency;
            set
            {
                if (_currency == null)
                {
                    _currency = value;
                    ValidationEvent += _currency.Validate;
                }
            }
        }
        public float? amount
        {
            get => _amount;

            set
            {
                if (_amount != value)
                {
                    if (_amount == null)
                    {
                        _amount = value;
                    }
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