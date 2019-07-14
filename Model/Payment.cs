using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    public class Payment : BaseFinType, IPayment
    {
        private Reference _sender;
        private Reference _receiver;
        private Amount _amount;
        private DateTime? _date;
        public DateTime? date
        {
            get => _date;
            set
            {
                if (_date == null)
                {
                    _date = value;
                    if (_date.Value != _date.Value.Date)
                    {
                        throw new Exception("Payment date should not specify time");
                    }
                }
            }
        }
        public int? contractId { get; set; }
        public string paymentType { get; set; }

        public Reference sender
        {
            get => _sender;
            set
            {
                if (_sender == null)
                {
                    _sender = value;
                    ValidationEvent += _sender.Validate;
                }
            }
        }
        public Reference receiver
        {
            get => _receiver;
            set
            {
                if (_receiver == null)
                {
                    _receiver = value;
                    ValidationEvent += _receiver.Validate;
                }
            }
        }

        public Amount amount
        {
            get => _amount;
            set
            {
                if (_amount == null)
                {
                    _amount = value;
                    ValidationEvent += _amount.Validate;
                }
            }
        }

        public void Validate()
        {
            CheckForNull();
            InvokeValidationEvent();
            if (!Enum.TryParse(paymentType, out PaymentTypes payType))
            {
                throw new Exception("Invalid type of payment");
            }
        }
        [OnDeserialized]
        public void Validate(StreamingContext context)
        {
            Validate();
        }

        public bool IsConsistentWithContract(IContract contract)
        {
            bool isConsistent = true;
            if (contractId != contract.id)
            {
                isConsistent = false;
            }
            if (!(sender.Equals(contract.buyerSellerModel.buyerPartyReference) &&
                receiver.Equals(contract.buyerSellerModel.sellerPartyReference)))
            {
                isConsistent = false;
            }
            if (contract.executionPeriodDates.startDate > date.Value ||
                contract.executionPeriodDates.expiryDate < date.Value)
            {
                isConsistent = false;
            }
            if (!amount.currency.Equals(contract.currency))
            {
                isConsistent = false;
            }
            if (amount.amount > contract.remainingAmount)
            {
                isConsistent = false;
            }
            return isConsistent;
        }

    }
}
