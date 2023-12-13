using System;
using System.Collections.Generic;
using System.Linq;

namespace BDMCommandLine
{
	public class CommandArgumentBase : ICommandArgument
	{
		#region Constructors
		/// <summary>
		/// Default contructor
		/// </summary>
		public CommandArgumentBase() { }

		/// <summary>
		/// Default contructor with type
		/// </summary>
		public CommandArgumentBase(ArgumentType type)
			: this()
			=> this.Type = type;

		/// <summary>
		/// Base contructor with no default
		/// </summary>
		public CommandArgumentBase(
			ArgumentType type,
			String name,
			String alias,
			String description,
			Boolean isRequired
		)
			: this()
		{
			this.Type = type;
			this.Name = name;
			this.Alias = alias;
			this.Description = description;
			this.IsRequired = isRequired;
		}

		/// <summary>
		/// Base contructor with default
		/// </summary>
		public CommandArgumentBase(
			ArgumentType type,
			String name,
			String alias,
			String description,
			Boolean isRequired,
			String defaultValue
		)
			: this()
		{
			this.Type = type;
			this.Name = name;
			this.Alias = alias;
			this.Description = description;
			this.IsRequired = isRequired;
			this.DefaultValue = defaultValue;
		}
		#endregion Constructors

		public ArgumentType Type { get; set; } = ArgumentType.Simple;
		public String Name { get; set; } = String.Empty;
		public String Description { get; set; } = String.Empty;
		public String Alias { get; set; } = String.Empty;
		public Boolean IsRequired { get; set; } = false;
		public ICommandArgumentOption[]? Options { get; set; }
		public virtual String MissingStatement { get; set; } = String.Empty;

		public Boolean IsMissing { get; set; } = true;
		public Boolean IsValid { get; set; } = false;
		public Boolean IsProvided { get; set; } = false;
		public String? DefaultValue { get; set; }
		public String? InvalidMessage { get; set; }

		public virtual ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = [];
			String argument = $"--{this.Name}";
			if (!String.IsNullOrWhiteSpace(this.Alias))
				argument += $", -{this.Alias}";
			returnValue.Add
			(
				this.IsRequired
					? ConsoleText.DarkRed($"{argument,-40}")
					: ConsoleText.DarkGreen($"{argument,-40}")
			);
			if (this.Type == ArgumentType.Flag)
				returnValue.Add(ConsoleText.White("[Flag] "));
			returnValue.Add(ConsoleText.White($"{this.Description}"));
			if (!String.IsNullOrEmpty(this.DefaultValue))
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.DarkMagenta($"{"",-45}Default: {this.DefaultValue}"));
			}
			if (this.Options is not null && this.Options.Length > 0)
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.Blue($"{"",-45}Options:"));
				foreach (ICommandArgumentOption option in this.Options)
				{
					returnValue.Add(ConsoleText.BlankLine());
					returnValue.Add(ConsoleText.Blue($"{"",-50}{option.Value,-12}   {option.Description}"));
				}
			}
			return [.. returnValue];
		}
		public virtual void ClearValue()
		{
			this.SimpleValue = null;
			this.OptionValue = null;
			this.ArrayValue = null;
			this.FlagValue = null;
		}
		public virtual void SetValue(String? value)
		{
			this.ClearValue();
			switch (this.Type)
			{
				case ArgumentType.Simple:
					this.SetSimpleValue(value);
					break;
				case ArgumentType.Option:
					this.SetOptionValue(value);
					break;
				case ArgumentType.Array:
					this.SetArrayValue(value);
					break;
				case ArgumentType.Flag:
					this.SetFlagValue(value);
					break;
			}
		}

		public virtual Object GetValue()
			=> this.Type switch
			{
				ArgumentType.Simple => this.GetSimpleValue(),
				ArgumentType.Option => this.GetOptionValue(),
				ArgumentType.Array => this.GetArrayValue(),
				ArgumentType.Flag => this.GetFlagValue(),
				_ => throw new Exception("Value not valid.")
			};
		public TEnum GetEnumValue<TEnum>() where TEnum : struct, IConvertible
		{
			TEnum returnValue;
			if (!typeof(TEnum).IsEnum)
				throw new ArgumentException("T must be an enumerated type");
			String? value = this.GetValue() as String;
			if (Enum.TryParse<TEnum>(value, true, out TEnum outValue1))
				returnValue = outValue1;
			else if (Enum.TryParse<TEnum>(this.DefaultValue, true, out TEnum outValue2))
				returnValue = outValue2;
			else
				returnValue = default;
			return returnValue;
		}

		#region SimpleValue
		/// <summary>
		/// Value with no default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="defaultValue"></param>
		public static CommandArgumentBase CreateSimpleArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired
		)
			=> new(ArgumentType.Simple, name, alias, description, isRequired);
		/// <summary>
		/// Value with default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="defaultValue"></param>
		public static CommandArgumentBase CreateSimpleArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			String defaultValue
		)
			=> new(ArgumentType.Simple, name, alias, description, isRequired, defaultValue);
		public String? SimpleValue { get; set; }
		public void SetSimpleValue(String? value)
		{
			this.ClearValue();
			if (value is null && this.IsRequired)
			{
				this.SimpleValue = null;
				this.IsMissing = true;
				if (this.DefaultValue is not null)
				{
					this.SimpleValue = this.DefaultValue;
					this.IsValid = true;
				}
				else
				{
					this.InvalidMessage = $"{this.Name}: must be supplied.";
					this.IsValid = false;
				}
			}
			else if (value is null && !this.IsRequired)
			{
				this.SimpleValue = null;
				this.IsMissing = true;
				this.IsValid = true;
			}
			else
			{
				this.IsMissing = false;
				this.SimpleValue = value;
				this.IsValid = true;
			}
		}
		public virtual String GetSimpleValue()
			=> this.SimpleValue ?? String.Empty;
		#endregion SimpleValue

		#region OptionValue
		/// <summary>
		/// Option with no default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="options"></param>
		public static CommandArgumentBase CreateOptionArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			ICommandArgumentOption[]? options
		)
			=> new(ArgumentType.Option, name, alias, description, isRequired) { Options = options };
		/// <summary>
		/// Option with default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="options"></param>
		/// <param name="defaultValue"></param>
		public static CommandArgumentBase CreateOptionArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			ICommandArgumentOption[]? options,
			String defaultValue
		)
			=> new(ArgumentType.Option, name, alias, description, isRequired, defaultValue) { Options = options };
		public String? OptionValue { get; set; }
		public void SetOptionValue(String? value)
		{
			this.ClearValue();
			if (value is null && this.IsRequired)
			{
				this.OptionValue = null;
				this.IsMissing = true;
				if (this.DefaultValue is not null)
				{
					this.OptionValue = this.DefaultValue;
					this.IsValid = true;
				}
				else
				{
					this.InvalidMessage = $"{this.Name}: must be supplied.";
					this.IsValid = false;
				}
			}
			else if (value is null && !this.IsRequired)
			{
				this.OptionValue = null;
				this.IsMissing = true;
				this.IsValid = true;
			}
			else
			{
				this.IsMissing = false;
				if (
					this.Options is not null
					&& this.Options.Any(o => o.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase))
				)
				{
					this.OptionValue = value;
					this.IsValid = false;
				}
				else
				{
					this.InvalidMessage = $"{this.Name}: one of the acceptable options must be provided.";
					this.IsValid = false;
				}
			}
		}
		public virtual String GetOptionValue()
			=> this.OptionValue ?? String.Empty;
		#endregion OptionValue

		#region ArrayValue
		/// <summary>
		/// Value with no default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="defaultValue"></param>
		public static CommandArgumentBase CreateArrayArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired
		)
			=> new(ArgumentType.Array, name, alias, description, isRequired);
		/// <summary>
		/// Value with default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="defaultValue"></param>
		public static CommandArgumentBase CreateArrayArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			String defaultValue
		)
			=> new(ArgumentType.Array, name, alias, description, isRequired, defaultValue);
		public String[]? ArrayValue { get; set; }
		public void SetArrayValue(String? value)
		{
			this.ClearValue();
			if (value is null && this.IsRequired)
			{
				this.OptionValue = null;
				this.IsMissing = true;
				if (this.DefaultValue is not null)
				{
					this.OptionValue = this.DefaultValue;
					this.IsValid = true;
				}
				else
				{
					this.InvalidMessage = $"{this.Name}: must be supplied.";
					this.IsValid = false;
				}
			}
			else if (value is null && !this.IsRequired)
			{
				this.OptionValue = null;
				this.IsMissing = true;
				this.IsValid = true;
			}
			else
			{
				this.IsMissing = false;
				if (value is not null)
					this.ArrayValue = Array.ConvertAll(value.Split(','), v => v.Trim());
				this.IsValid = true;
			}
		}
		public virtual String[] GetArrayValue()
			=> this.ArrayValue ?? [];
		#endregion ArrayValue

		#region FlagValue
		/// <summary>
		/// Flag with no default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		public static CommandArgumentBase CreateFlagArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired
		)
			=> new(ArgumentType.Flag, name, alias, description, isRequired);
		/// <summary>
		/// Flag with no default
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="description"></param>
		/// <param name="isRequired"></param>
		/// <param name="defaultValue"></param>
		public static CommandArgumentBase CreateFlagArgument(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			Boolean defaultValue
		)
			=> new(ArgumentType.Flag, name, alias, description, isRequired, defaultValue.ToString().ToLower());
		public Boolean? FlagValue { get; set; }
		public void SetFlagValue(String? value)
		{
			value ??= "true";
			if (value is null && this.IsRequired)
			{
				this.FlagValue = null;
				this.IsMissing = true;
				if (this.DefaultValue is not null)
				{
					this.FlagValue = this.DefaultValue?.Equals("true", StringComparison.InvariantCultureIgnoreCase);
					this.IsValid = true;
				}
				else
				{
					this.InvalidMessage = $"{this.Name}: \"{value}\" is not an acceptable option. Must be \"true\" or \"false\".";
					this.IsValid = false;
				}
			}
			else if (value is null && !this.IsRequired)
			{
				this.FlagValue = null;
				this.IsMissing = true;
				this.IsValid = true;
			}
			else
			{
				this.IsMissing = false;
				this.FlagValue = value switch
				{
					"true" or "yes" or "1" => true,
					"false" or "no" or "0" => false,
					_ => null
				};
				if (this.FlagValue is null)
				{
					this.InvalidMessage = $"{this.Name}: \"{value}\" is not an acceptable option. Must be \"true\" or \"false\".";
					this.IsValid = false;
				}
			}
		}
		public virtual Boolean GetFlagValue()
			=> this.FlagValue ?? false;
		#endregion FlagValue

		public override String ToString()
			=> this.Type switch
			{
				ArgumentType.Simple => this.GetSimpleValue().ToString(),
				ArgumentType.Option => this.GetOptionValue().ToString(),
				ArgumentType.Array => String.Join(", ", this.GetArrayValue()),
				ArgumentType.Flag => this.GetFlagValue().ToString(),
				_ => ""
			};
	}
}
