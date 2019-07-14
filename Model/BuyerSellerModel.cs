using System;

namespace Model
{
    public class BuyerSellerModel: BaseFinType, IValidable
    {
        private Reference _buyerPartyReference;
        public Reference buyerPartyReference
        {
            get => _buyerPartyReference;
            set
            {
                if (_buyerPartyReference == null)
                {
                    _buyerPartyReference = value;
                    ValidationEvent += _buyerPartyReference.Validate;
                }
            }
        }

        private Reference _sellerPartyReference;
        public Reference sellerPartyReference
        { get => _sellerPartyReference;
            set
            {
                if (_sellerPartyReference == null)
                {
                    _sellerPartyReference = value;
                    ValidationEvent += _sellerPartyReference.Validate;
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