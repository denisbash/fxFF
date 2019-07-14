using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    public class BaseFinType
    {
        public static float epsilon = 1e-6f;
        public event ValidationDelegate ValidationEvent;

        public void InvokeValidationEvent()
        {
            //begin
            var x = ValidationEvent;
            //end
            ValidationEvent.Invoke();
        }
        
        public void CheckForNull()
        {
            var props = this.GetType().GetProperties().Where(prop =>
                !Attribute.IsDefined(prop, typeof(IgnoreDataMemberAttribute)));
            foreach (var prop in props)
            {
                var x = prop.GetValue(this);
                if (prop.GetValue(this) == null)
                {                    
                    throw new Exception($"Property {prop.Name} is null");
                }
            }
        }
    }
}
