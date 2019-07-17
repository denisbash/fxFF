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
        public DateTime? Date
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
        [DataMember(Name ="contractId")]
        public int? ContractId { get; set; }
        [DataMember(Name ="paymentType")]
        public string PaymentType { get; set; }
        [DataMember(Name ="sender")]
        public Reference Sender
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
        [DataMember(Name ="receiver")]
        public Reference Receiver
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
        [DataMember(Name ="amount")]
        public Amount Amount
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
            if (!Enum.TryParse(PaymentType, out PaymentTypes payType))
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
            if (ContractId != contract.Id)
            {
                isConsistent = false;
            }
            if (!(Sender.Equals(contract.BuyerSellerModel.BuyerPartyReference) &&
                Receiver.Equals(contract.BuyerSellerModel.SellerPartyReference)))
            {
                isConsistent = false;
            }
            if (contract.ExecutionPeriodDates.StartDate > Date.Value ||
                contract.ExecutionPeriodDates.ExpiryDate < Date.Value)
            {
                isConsistent = false;
            }
            if (!Amount.Currency.Equals(contract.Currency))
            {
                isConsistent = false;
            }
            if (Amount.amount > contract.RemainingAmount)
            {
                isConsistent = false;
            }
            return isConsistent;
        }

    }
}
