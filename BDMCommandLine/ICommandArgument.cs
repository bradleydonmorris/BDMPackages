using System;

namespace BDMCommandLine
{
	public enum ArgumentType
	{
		Simple = 0,
		Flag = 1,
		Array = 2,
		Option = 3
	}

    public interface ICommandArgument
    {
		public ArgumentType Type { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public String Alias { get; set; }
		public Boolean IsRequired { get; set; }
		public ICommandArgumentOption[]? Options { get; set; }
		public String MissingStatement { get; set; }

		public Boolean IsMissing { get; set; }
		public Boolean IsValid { get; set; }
		public Boolean IsProvided { get; set; }
		public String? DefaultValue { get; set; }
		public String? InvalidMessage { get; set; }

		public ConsoleText[] GetHelpText();
		public void ClearValue();
		public void SetValue(String? value);
		public Object GetValue();
		public TEnum GetEnumValue<TEnum>() where TEnum : struct, IConvertible;

		public String? SimpleValue { get; set; }
		public void SetSimpleValue(String? value);
		public String GetSimpleValue();


		public String? OptionValue { get; set; }
		public void SetOptionValue(String? value);
		public String GetOptionValue();


		public String[]? ArrayValue { get; set; }
		public void SetArrayValue(String? value);
		public String[] GetArrayValue();


		public Boolean? FlagValue { get; set; }
		public void SetFlagValue(String? value);
		public Boolean GetFlagValue();
	}
}

