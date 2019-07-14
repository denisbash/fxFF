using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ExecutionPeriodDates: BaseFinType, IValidable
    {
        public int id { get; set; }

        public DateTime startDate { get; set; }
        public DateTime expiryDate { get; set; }

        private BusinessCenter[] _businessCenters;
        public BusinessCenter[] businessCenters
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
            if (startDate >= expiryDate)
            {
                throw new Exception("Start date cannot follow expiry date");
            }
            InvokeValidationEvent();
        }

        
    }
}
