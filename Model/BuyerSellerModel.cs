using System;
using System.Runtime.Serialization;

namespace Model
{
    public class BuyerSellerModel: BaseFinType, IValidable
    {
        private Reference _buyerPartyReference;
        [DataMember(Name ="buyerPartyReference")]
        public Reference BuyerPartyReference
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
        [DataMember(Name ="sellerPartyReference")]
        public Reference SellerPartyReference
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