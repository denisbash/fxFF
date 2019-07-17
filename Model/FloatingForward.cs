using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    public class FloatingForward : BaseFinType, IValidable, IContract
    {
        public int? Id { get; set; }
        [IgnoreDataMember]
        public Currency Currency => PutCallCurrencyModel.CallCurrency;

        private ForwardRate _forwardRate;
        [DataMember(Name ="forwardRate")]
        public ForwardRate ForwardRate
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
        [DataMember(Name ="earliestExecutionTime")]
        public ExecutionTime EarliestExecutionTime
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
        [DataMember(Name ="latestExecutionTime")]
        public ExecutionTime LatestExecutionTime
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
        [DataMember(Name ="notionalAmount")]
        public Amount NotionalAmount
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
        [DataMember(Name ="executionPeriodDates")]
        public ExecutionPeriodDates ExecutionPeriodDates
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
        [JsonProperty(PropertyName ="putCallCurrency.model")]
        public PutCallCurrencyModel PutCallCurrencyModel
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
        [JsonProperty(PropertyName ="buyerSeller.model")]
        public BuyerSellerModel BuyerSellerModel
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
        public float? RemainingAmount { get; set; }

        [OnDeserialized]
        public void ValidateAndFinalize(StreamingContext context)
        {
            Validate();
            RemainingAmount = NotionalAmount.amount;
        }

        public void Validate()
        {
            CheckForNull();
            if (EarliestExecutionTime.IsGreater(LatestExecutionTime))
            {
                throw new Exception("Execution times are out of order");
            }
            if (!(PutCallCurrencyModel.PutCurrency.Equals(ForwardRate.Currency1) &&
                PutCallCurrencyModel.CallCurrency.Equals(ForwardRate.Currency2)))
            {
                throw new Exception("Currencies in forwardRate and putCallCurrencyModel" +
                    "do not match");
            }

            InvokeValidationEvent();
        }    
        
    }
}
