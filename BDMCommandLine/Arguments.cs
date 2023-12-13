using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BDMCommandLine
{
	public class Arguments : IArguments
	{
		public Arguments() { }

		private readonly List<ICommandArgument> _Arguments = [];

		public IEnumerator<ICommandArgument> GetEnumerator()
			=> this._Arguments.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> this._Arguments.GetEnumerator();

		public ICommandArgument this[Int32 index] => this._Arguments[index];
		public ICommandArgument? this[String nameOrAlias] => this.Get(nameOrAlias);

		public Int32 Count => this._Arguments.Count;

		public Int32 IndexOf(String nameOrAlias)
			=> this._Arguments.FindIndex(c =>
			c.Name.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)
			|| c.Alias.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase));

		public Boolean Contains(String nameOrAlias)
			=> this._Arguments.Any(c =>
			c.Name.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)
			|| c.Alias.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase));

		public ICommandArgument? Get(String nameOrAlias)
			=> this._Arguments.FirstOrDefault(c =>
			c.Name.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)
			|| c.Alias.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase));

		public void Add(ICommandArgument value)
			=> this._Arguments.Add(value);

		public void AddRange(IEnumerable<ICommandArgument> values)
			=> this._Arguments.AddRange(values);

		public Boolean TryGetEnumValue<TEnum>(String nameOrAlias, out TEnum result) where TEnum : struct, IConvertible
		{
			if (!typeof(TEnum).IsEnum)
				throw new ArgumentException("T must be an enumerated type");
			Boolean returnValue;
			if (this.Contains(nameOrAlias))
			{
				if (Enum.TryParse<TEnum>(this[nameOrAlias]?.GetValue() as String, true, out TEnum outValue1))
				{
					result = outValue1;
					returnValue = true;
				}
				else if (Enum.TryParse<TEnum>(this[nameOrAlias]?.DefaultValue, true, out TEnum outValue2))
				{
					result = outValue2;
					returnValue = true;
				}
				else
				{
					result = default;
					returnValue = false;
				}
			}
			else
			{
				result = default;
				returnValue = false;
			}
			return returnValue;
		}
		public TEnum GetEnumValue<TEnum>(String nameOrAlias) where TEnum : struct, IConvertible
		{
			if (!typeof(TEnum).IsEnum)
				throw new ArgumentException("T must be an enumerated type");
			TEnum returnValue = default;
			if (this.Contains(nameOrAlias))
			{
				if (Enum.TryParse<TEnum>(this[nameOrAlias]?.GetValue() as String, true, out TEnum outValue1))
					returnValue = outValue1;
				else if (Enum.TryParse<TEnum>(this[nameOrAlias]?.DefaultValue, true, out TEnum outValue2))
					returnValue = outValue2;
			}
			return returnValue;
		}
		public TEnum GetEnumValue<TEnum>(String nameOrAlias, TEnum defaultValue) where TEnum : struct, IConvertible
		{
			if (!typeof(TEnum).IsEnum)
				throw new ArgumentException("T must be an enumerated type");
			TEnum returnValue = defaultValue;
			if (this.Contains(nameOrAlias))
			{
				if (Enum.TryParse<TEnum>(this[nameOrAlias]?.GetValue() as String, true, out TEnum outValue1))
					returnValue = outValue1;
				else if (Enum.TryParse<TEnum>(this[nameOrAlias]?.DefaultValue, true, out TEnum outValue2))
					returnValue = outValue2;
			}
			else
				returnValue = default;
			return returnValue;
		}

		public Boolean TryGetValue(String nameOrAlias, out Object result)
		{
			Boolean returnValue;
			if (this.Contains(nameOrAlias) && this[nameOrAlias] is ICommandArgument commandArgument)
			{
				result = commandArgument.Type switch
				{
					ArgumentType.Simple => commandArgument.GetSimpleValue(),
					ArgumentType.Option => commandArgument.GetOptionValue(),
					ArgumentType.Array => commandArgument.GetArrayValue(),
					ArgumentType.Flag => commandArgument.GetFlagValue(),
					_ => throw new Exception("Value not valid.")
				};
				returnValue = true;
			}
			else
			{
				result = String.Empty;
				returnValue = false;
			}
			return returnValue;
		}
		public Object? GetValue(String nameOrAlias)
		{
			Object? returnValue = null;
			if (this.Contains(nameOrAlias) && this[nameOrAlias] is ICommandArgument commandArgument)
				returnValue = commandArgument.Type switch
				{
					ArgumentType.Simple => commandArgument.GetSimpleValue(),
					ArgumentType.Option => commandArgument.GetOptionValue(),
					ArgumentType.Array => commandArgument.GetArrayValue(),
					ArgumentType.Flag => commandArgument.GetFlagValue(),
					_ => throw new Exception("Value not valid.")
				};
			return returnValue;
		}
		public Object? GetValue(String nameOrAlias, Object defaultValue)
		{
			Object? returnValue = defaultValue;
			if (this.Contains(nameOrAlias) && this[nameOrAlias] is ICommandArgument commandArgument)
				returnValue = commandArgument.Type switch
				{
					ArgumentType.Simple => commandArgument.GetSimpleValue(),
					ArgumentType.Option => commandArgument.GetOptionValue(),
					ArgumentType.Array => commandArgument.GetArrayValue(),
					ArgumentType.Flag => commandArgument.GetFlagValue(),
					_ => throw new Exception("Value not valid.")
				};
			return returnValue;
		}

		public Boolean TryGetSimpleValue(String nameOrAlias, out String result)
		{
			Boolean returnValue;
			if (this.Contains(nameOrAlias))
			{
				result = this[nameOrAlias]?.GetSimpleValue() ?? String.Empty;
				returnValue = true;
			}
			else
			{
				result = String.Empty;
				returnValue = false;
			}
			return returnValue;
		}
		public String GetSimpleValue(String nameOrAlias)
		{
			String returnValue = String.Empty;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetSimpleValue() ?? String.Empty;
			return returnValue;
		}
		public String GetSimpleValue(String nameOrAlias, String defaultValue)
		{
			String returnValue = defaultValue;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetSimpleValue() ?? String.Empty;
			return returnValue;
		}


		public Boolean TryGetOptionValue(String nameOrAlias, out String result)
		{
			Boolean returnValue;
			if (this.Contains(nameOrAlias))
			{
				result = this[nameOrAlias]?.GetOptionValue() ?? String.Empty;
				returnValue = true;
			}
			else
			{
				result = String.Empty;
				returnValue = false;
			}
			return returnValue;
		}
		public String GetOptionValue(String nameOrAlias)
		{
			String returnValue = String.Empty;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetOptionValue() ?? String.Empty;
			return returnValue;
		}
		public String GetOptionValue(String nameOrAlias, String defaultValue)
		{
			String returnValue = defaultValue;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetOptionValue() ?? String.Empty;
			return returnValue;
		}

		public Boolean TryGetArrayValue(String nameOrAlias, out String[] result)
		{
			Boolean returnValue;
			if (this.Contains(nameOrAlias))
			{
				result = this[nameOrAlias]?.GetArrayValue() ?? [];
				returnValue = true;
			}
			else
			{
				result = [];
				returnValue = false;
			}
			return returnValue;
		}
		public String[] GetArrayValue(String nameOrAlias)
		{
			String[] returnValue = [];
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetArrayValue() ?? [];
			return returnValue;
		}
		public String[] GetArrayValue(String nameOrAlias, String[] defaultValue)
		{
			String[] returnValue = defaultValue;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetArrayValue() ?? [];
			return returnValue;
		}

		public Boolean TryGetFlagValue(String nameOrAlias, out Boolean result)
		{
			Boolean returnValue;
			if (this.Contains(nameOrAlias))
			{
				result = this[nameOrAlias]?.GetFlagValue() ?? false;
				returnValue = true;
			}
			else
			{
				result = false;
				returnValue = false;
			}
			return returnValue;
		}
		public Boolean GetFlagValue(String nameOrAlias)
		{
			Boolean returnValue = false;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetFlagValue() ?? false;
			return returnValue;
		}
		public Boolean GetFlagValue(String nameOrAlias, Boolean defaultValue)
		{
			Boolean returnValue = defaultValue;
			if (this.Contains(nameOrAlias))
				returnValue = this[nameOrAlias]?.GetFlagValue() ?? false;
			return returnValue;
		}
	}
}