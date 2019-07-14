using System;

namespace Model
{
    public interface IPayment: IValidable
    {
        Amount amount { get; set; }
        int? contractId { get; set; }
        DateTime? date { get; set; }
        string paymentType { get; set; }
        Reference receiver { get; set; }
        Reference sender { get; set; }

        bool IsConsistentWithContract(IContract contract);
    }
}