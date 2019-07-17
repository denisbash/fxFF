using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    
    public class ForwardRate: BaseFinType, IValidable
    {
        private float? _rate;
        private Currency _currency1;
        private Currency _currency2;
        private QuoteBasisEnum _quoteBasis;        
        public float? rate
        {
            get => _rate;
            set
            {
                if (_rate == null)
                {
                    if (value <= 0)
                    {
                        throw new Exception("Invalid value for rate");
                    }
                    else
                    {
                        _rate = value;
                    }
                }
            }
        }
        
        [DataMember(Name ="currency1")]
        public Currency Currency1
        {
            get => _currency1;
            set
            {
                if (_currency1 == null)
                {
                    _currency1 = value;
                    ValidationEvent += Currency1.Validate;
                }
            }
        }
        [DataMember(Name ="currency2")]
        public Currency Currency2
        {
            get => _currency2;
            set
            {
                if (_currency2 == null)
                {
                    _currency2 = value;
                    ValidationEvent += Currency2.Validate;
                }
            }
        }
        [DataMember(Name ="quoteBasis")]
        public QuoteBasisEnum QuoteBasis
        {
            get => _quoteBasis;
            set
            {
                if (_quoteBasis == null)
                {
                    _quoteBasis = value;
                    ValidationEvent += _quoteBasis.Validate;
                }
            }
        }

        
        
        public void Validate()
        {
            CheckForNull();
            InvokeValidationEvent();
            if (Math.Abs((float)rate - (float)QuoteBasis.GetRate()) >= epsilon)
            {

            }
        }
    }
}
