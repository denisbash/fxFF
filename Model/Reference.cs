using System;

namespace Model
{
    public class Reference: BaseFinType, IValidable, IEquatable<Reference>
    {
        public string href { get; set; }

        public bool Equals(Reference other) => href == other.href;
        

        public void Validate()
        {
            if (string.IsNullOrEmpty(href))
            {
                throw new Exception($"Ref of {this.GetType()} cannot be null or empty");
            }
        }
    }
}