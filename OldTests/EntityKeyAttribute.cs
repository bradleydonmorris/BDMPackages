using System;

namespace SARTeam.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityKeyAttribute : Attribute
    {
        public String? CompositeKeyGroup {get;set;}
        public EntityKeyAttribute(String? compositeKeyGroup = null)
            => this.CompositeKeyGroup = compositeKeyGroup;
    }
}
