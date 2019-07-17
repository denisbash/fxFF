using System;
using System.Runtime.Serialization;

namespace Model
{
    public class Amount: BaseFinType, IValidable
    {
        private float? _amount;

        private Currency _currency;
        [DataMember(Name ="currency")]
        public Currency Currency
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
        [DataMember]
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