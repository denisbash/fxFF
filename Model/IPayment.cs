using System;
using System.Runtime.Serialization;

namespace Model
{
    public interface IPayment: IValidable
    {
        [DataMember(Name ="amount")]
        Amount Amount { get; set; }
        [DataMember(Name ="contractId")]
        int? ContractId { get; set; }
        [DataMember(Name ="date")]
        DateTime? Date { get; set; }
        [DataMember(Name ="paymentType")]
        string PaymentType { get; set; }
        [DataMember(Name ="receiver")]
        Reference Receiver { get; set; }
        [DataMember(Name ="sender")]
        Reference Sender { get; set; }

        bool IsConsistentWithContract(IContract contract);
    }
}