using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    public class ExecutionTime: TimeClass, IValidable
    {
        
        private TimeSpan _hourMinuteTime;
        [DataMember(Name ="hourMinuteTime")]
        public TimeSpan HourMinuteTime
        {
            get => _hourMinuteTime;
            set
            {
                if (_hourMinuteTime != value)
                {
                    if (value.Seconds != 0) throw new Exception("Seconds must be set to zero");
                    _hourMinuteTime = value;
                }
            }
        }

        public override TimeSpan Time => _hourMinuteTime;

        private BusinessCenter _businessCenter;
        [DataMember(Name ="businessCenter")]
        public BusinessCenter BusinessCenter
        {
            get => _businessCenter;
            set
            {
                if (_businessCenter == null)
                {
                    _businessCenter = value;
                    ValidationEvent += _businessCenter.Validate;
                }
            }
        }

        //public bool IsGreaterThan(DateTime date) => hourMinuteTime > date;
        //public bool IsLessThan(DateTime date) => hourMinuteTime < date;

        public void Validate()
        {
            CheckForNull();
            InvokeValidationEvent();
        }

        
    }
}
