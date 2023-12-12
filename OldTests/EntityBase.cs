using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SARTeam.Models
{
	public class EntityBase
	{
		[EntityKey()]
		[JsonProperty("KeyGUID")]
		public Guid KeyGUID { get; set; }

		public EntityBase()
		{
			this.KeyGUID = Guid.Empty;
		}

		public IEnumerable<EntityKey> GetEntityKeys()
		{
			System.Reflection.PropertyInfo[] propertyInfos = this
				.GetType()
				.GetProperties()
				.Where(prop => Attribute.IsDefined(prop, typeof(EntityKeyAttribute)))
				.ToArray();
			foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
				yield return new(this, propertyInfo);
		}

		public IEnumerable<EntityKey> GetEntityKeys(String compositeKeyGroup)
		{
			System.Reflection.PropertyInfo[] propertyInfos = this
				.GetType()
				.GetProperties()
				.Where(prop => Attribute.IsDefined(prop, typeof(EntityKeyAttribute)))
				.ToArray();
			foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
				if (
					propertyInfo.GetCustomAttributes(typeof(EntityKeyAttribute), true).FirstOrDefault() is EntityKeyAttribute entityKeyAttribute
					&& entityKeyAttribute.CompositeKeyGroup == compositeKeyGroup
				)
					yield return new(this, propertyInfo);
		}

		public virtual Boolean AreKeysEqual(EntityBase? other)
		{
			Boolean returnValue = false;
			if (other is null)
				returnValue = false;
			if (ReferenceEquals(this, other))
				returnValue = true;
			if (
				other is not null
				&& typeof(EntityBase).IsAssignableFrom(other.GetType())
			)
			{
				List<Boolean> compares = new();
				EntityKey[] otherKeys = other
					.GetEntityKeys()
					.ToArray();
				foreach (EntityKey thisKey in this.GetEntityKeys().ToArray())
				{
					EntityKey otherKey = otherKeys.First(k => k.PropertyName == thisKey.PropertyName);
					if (otherKey is not null)
						if (
							otherKey.Value is Guid otherKeyGUID
							&& thisKey.Value is Guid thisKeyGUID
							&& otherKeyGUID.Equals(thisKeyGUID)
						)
							compares.Add(true);
						else if (otherKey.Value == thisKey.Value)
							compares.Add(true);
						else
							compares.Add(false);
					else
						compares.Add(false);
				}
				if (compares.Any(v => v == false))
					returnValue = false;
				else
					returnValue = true;
			}
			else
				returnValue = false;
			return returnValue;
		}

		public virtual Boolean AreKeysEqual(EntityBase? other, String compositeKeyGroup)
		{
			Boolean returnValue = false;
			if (other is null)
				returnValue = false;
			if (ReferenceEquals(this, other))
				returnValue = true;
			if (
				other is not null
				&& typeof(EntityBase).IsAssignableFrom(other.GetType())
			)
			{
				List<Boolean> compares = new();
				//CompositeKeyGroup
				EntityKey[] otherKeys = other
					.GetEntityKeys(compositeKeyGroup)
					.ToArray();
				foreach (EntityKey thisKey in this.GetEntityKeys(compositeKeyGroup).ToArray())
				{
					EntityKey otherKey = otherKeys.First(k => k.PropertyName == thisKey.PropertyName);
					if (otherKey is not null)
						if (
							otherKey.Value is Guid otherKeyGUID
							&& thisKey.Value is Guid thisKeyGUID
							&& otherKeyGUID.Equals(thisKeyGUID)
						)
							compares.Add(true);
						else if (otherKey.Value == thisKey.Value)
							compares.Add(true);
						else
							compares.Add(false);
					else
						compares.Add(false);
				}
				if (compares.Any(v => v == false))
					returnValue = false;
				else
					returnValue = true;
			}
			else
				returnValue = false;
			return returnValue;
		}
	}
}
