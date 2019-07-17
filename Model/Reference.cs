using System;
using System.Runtime.Serialization;

namespace Model
{
    public class Reference: BaseFinType, IValidable, IEquatable<Reference>
    {
        [DataMember(Name ="href")]
        public string Href { get; set; }

        public bool Equals(Reference other) => Href == other.Href;
        

        public void Validate()
        {
            if (string.IsNullOrEmpty(Href))
            {
                throw new Exception($"Ref of {this.GetType()} cannot be null or empty");
            }
        }
    }
}