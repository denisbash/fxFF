using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Model
{
    public class ExecutionPeriodDates: BaseFinType, IValidable
    {
        [DataMember(Name ="id")]
        public int Id { get; set; }
        [DataMember(Name ="startDate")]
        public DateTime StartDate { get; set; }
        [DataMember(Name ="expiryDate")]
        public DateTime ExpiryDate { get; set; }

        private BusinessCenter[] _businessCenters;
        [DataMember(Name ="businessCenters")]
        public BusinessCenter[] BusinessCenters
        {
            get => _businessCenters;
            set
            {
                if (_businessCenters == null)
                {
                    _businessCenters = value;
                    foreach (var businessCenter in _businessCenters)
                    {
                        ValidationEvent += businessCenter.Validate;
                    }
                }
            }
        }

        public void Validate()
        {
            CheckForNull();
            if (StartDate >= ExpiryDate)
            {
                throw new Exception("Start date cannot follow expiry date");
            }
            InvokeValidationEvent();
        }

        
    }
}
