using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    public class BusinessCenter: BaseFinType, IValidable
    {
        [DataMember(Name ="businessCenterScheme")]
        public string BusinessCenterScheme { get; set; }
        [DataMember(Name ="id")]
        public int? Id { get; set; }

        public void Validate()
        {
            CheckForNull();            
            if (string.IsNullOrEmpty(BusinessCenterScheme))
            {
                throw new Exception("BusinessCenterScheme cannot be null or empty");
            }
        }
    }
}
