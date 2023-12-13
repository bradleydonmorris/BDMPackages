using System;
using System.Collections.Generic;

namespace BDMCommandLine
{
	public interface IArguments : IEnumerable<ICommandArgument>
	{
		public Int32 Count { get; }
		public Int32 IndexOf(String nameOrAlias);
		public Boolean Contains(String nameOrAlias);
		public ICommandArgument? Get(String nameOrAlias);
		public void Add(ICommandArgument value);
		public void AddRange(IEnumerable<ICommandArgument> values);

		public Boolean TryGetEnumValue<TEnum>(String nameOrAlias, out TEnum result) where TEnum : struct, IConvertible;
		public TEnum GetEnumValue<TEnum>(String nameOrAlias) where TEnum : struct, IConvertible;
		public TEnum GetEnumValue<TEnum>(String nameOrAlias, TEnum defaultValue) where TEnum : struct, IConvertible;

		public Boolean TryGetValue(String nameOrAlias, out Object result);

		public Object? GetValue(String nameOrAlias);
		public Object? GetValue(String nameOrAlias, Object defaultValue);

		public Boolean TryGetSimpleValue(String nameOrAlias, out String result);
		public String GetSimpleValue(String nameOrAlias);
		public String GetSimpleValue(String nameOrAlias, String defaultValue);

		public Boolean TryGetOptionValue(String nameOrAlias, out String result);
		public String GetOptionValue(String nameOrAlias);
		public String GetOptionValue(String nameOrAlias, String defaultValue);

		public Boolean TryGetArrayValue(String nameOrAlias, out String[] result);
		public String[] GetArrayValue(String nameOrAlias);
		public String[] GetArrayValue(String nameOrAlias, String[] defaultValue);

		public Boolean TryGetFlagValue(String nameOrAlias, out Boolean result);
		public Boolean GetFlagValue(String nameOrAlias);
		public Boolean GetFlagValue(String nameOrAlias, Boolean defaultValue);
	}
}