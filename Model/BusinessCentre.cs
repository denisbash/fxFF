using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BusinessCenter: BaseFinType, IValidable
    {
        public string businessCenterScheme { get; set; }
        public int? id { get; set; }

        public void Validate()
        {
            CheckForNull();            
            if (string.IsNullOrEmpty(businessCenterScheme))
            {
                throw new Exception("BusinessCenterScheme cannot be null or empty");
            }
        }
    }
}
