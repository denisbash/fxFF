using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    public class FloatingForward : BaseFinType, IValidable, IContract
    {
        public int? id { get; set; }
        [IgnoreDataMember]
        public Currency currency => putCallCurrencyModel.callCurrency;
        private ForwardRate _forwardRate;
        public ForwardRate forwardRate
        {
            get => _forwardRate;
            set
            {
                if (_forwardRate == null)
                {
                    _forwardRate = value;
                    ValidationEvent += _forwardRate.Validate;
                }
                
            }
            
        }
        private ExecutionTime _earliestExecutionTime;
        public ExecutionTime earliestExecutionTime
        {
            get => _earliestExecutionTime;
            set
            {
                if (_earliestExecutionTime == null)
                {
                    _earliestExecutionTime = value;
                    ValidationEvent += _earliestExecutionTime.Validate;
                }
            } 
        }

        private ExecutionTime _latestExecutionTime;
        public ExecutionTime latestExecutionTime
        {
            get => _latestExecutionTime;
            set
            {
                if (_latestExecutionTime == null)
                {
                    _latestExecutionTime = value;
                    ValidationEvent += _latestExecutionTime.Validate;
                }
                
            }            
        }

        private Amount _notionalAmount;
        public Amount notionalAmount
        {
            get => _notionalAmount;
            set
            {
                if (_notionalAmount == null)
                {
                    _notionalAmount = value;
                    ValidationEvent += _notionalAmount.Validate;
                }
            }
        }

        private ExecutionPeriodDates _executionPeriodDates;
        public ExecutionPeriodDates executionPeriodDates
        {
            get => _executionPeriodDates;
            set
            {
                if (_executionPeriodDates == null)
                {
                    _executionPeriodDates = value;
                    ValidationEvent += _executionPeriodDates.Validate;
                }                
            }
        }

        private PutCallCurrencyModel _putCallCurrencyModel;
        public PutCallCurrencyModel putCallCurrencyModel
        {
            get => _putCallCurrencyModel;
            set
            {
                if (_putCallCurrencyModel == null)
                {
                    _putCallCurrencyModel = value;
                    ValidationEvent += _putCallCurrencyModel.Validate;
                }
            }
        }

        private BuyerSellerModel _buyerSellerModel;
        public BuyerSellerModel buyerSellerModel
        {
            get => _buyerSellerModel;
            set
            {
                if (_buyerSellerModel == null)
                {
                    _buyerSellerModel = value;
                    ValidationEvent += _buyerSellerModel.Validate;
                }
            }
        }

        [IgnoreDataMember]
        public float? remainingAmount { get; set; }

        [OnDeserialized]
        public void ValidateAndFinalize(StreamingContext context)
        {
            Validate();
            remainingAmount = notionalAmount.amount;
        }

        public void Validate()
        {
            CheckForNull();
            if (earliestExecutionTime.IsGreater(latestExecutionTime))
            {
                throw new Exception("Execution times are out of order");
            }
            if (!(putCallCurrencyModel.putCurrency.Equals(forwardRate.currency1) &&
                putCallCurrencyModel.callCurrency.Equals(forwardRate.currency2)))
            {
                throw new Exception("Currencies in forwardRate and putCallCurrencyModel" +
                    "do not match");
            }

            InvokeValidationEvent();
        }    
        
    }
}
