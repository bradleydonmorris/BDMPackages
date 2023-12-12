using System;
using System.Linq;

namespace SARTeam.Models
{
	public class EntityKey
	{
		public String? CompositeKeyGroup { get; set; }
		public String PropertyName { get; set; }
		public Object? Value { get; set; }

		public EntityKey()
		{
			this.PropertyName = String.Empty;
		}
		public EntityKey(String propertyName, Object? value, String? compositeKeyGroup)
		{
			this.PropertyName = propertyName;
			this.Value = value;
			this.CompositeKeyGroup = compositeKeyGroup;
		}

		public EntityKey(Object caller, System.Reflection.PropertyInfo propertyInfo)
		{
			if (propertyInfo.GetCustomAttributes(typeof(EntityKeyAttribute), true).FirstOrDefault() is EntityKeyAttribute entityKeyAttribute)
			{
				this.PropertyName = propertyInfo.Name;
				this.Value = propertyInfo.GetValue(caller);
				this.CompositeKeyGroup = entityKeyAttribute.CompositeKeyGroup;
			}
			else
				throw new ArgumentException("EntityKeyAttribute not defined");

		}
	}
}
