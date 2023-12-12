#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDMCommandLine
{
	public class CommandArgumentBase : ICommandArgument
	{
		private readonly String _Name;
		public String Name => this._Name;

		private readonly String _Description;
		public String Description => this._Description;

		private readonly String _Alias;
		public String Alias => this._Alias;

		private readonly Boolean _IsRequired;
		public Boolean IsRequired => this._IsRequired;

		private readonly Boolean _IsFlag;
		public Boolean IsFlag => this._IsFlag;

		private readonly ICommandArgumentOption[]? _Options;
		public ICommandArgumentOption[]? Options => this._Options;

		private readonly String _MissingStatement;
		public virtual String MissingStatement => this._MissingStatement;


		private Boolean _IsMissing;
		public Boolean IsMissing => this._IsMissing;

		private Boolean _IsValid;
		public Boolean IsValid => this._IsValid;

		private Boolean _IsProvided;
		public Boolean IsProvided => this._IsProvided;

		private String? _Value;
		public String? Value => this._Value;

		private readonly String? _DefaultValue;
		public String? DefaultValue => this._DefaultValue;

		private String? _InvalidMessage;
		public String? InvalidMessage => this._InvalidMessage;

		public Boolean IsFlagedTrue => (this.IsFlag && this.IsProvided);


		//This should never be hit
		public CommandArgumentBase()
		{
			this._Name = String.Empty;
			this._Alias = String.Empty;
			this._Description = String.Empty;
			this._IsRequired = false;
			this._IsFlag = false;
			this._Options = null;
			this._MissingStatement = $"{this._Name} was not provided and is required.";
			this._IsValid = false;
			this._IsProvided = false;
			this._IsFlag = false;
		}

		public CommandArgumentBase(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			Boolean isFlag,
			ICommandArgumentOption[]? options,
			String missingStatement,
			String defaultValue
		)
		{
			this._Name = name;
			this._Alias = alias;
			this._Description = description;
			this._IsRequired = isRequired;
			this._IsFlag = isFlag;
			this._Options = options;
			this._MissingStatement = missingStatement;
			this._DefaultValue = defaultValue;
			this._InvalidMessage = null;
			if (!String.IsNullOrWhiteSpace(this._DefaultValue))
				this._Value = this._DefaultValue;
			this._IsMissing = true;
			this._IsValid = false;
			this._IsProvided = false;
		}

		public CommandArgumentBase(
			String name,
			String alias,
			String description,
			Boolean isRequired,
			Boolean isFlag,
			ICommandArgumentOption[]? options,
			String defaultValue
		)
		{
			this._Name = name;
			this._Alias = alias;
			this._Description = description;
			this._IsRequired = isRequired;
			this._IsFlag = isFlag;
			this._Options = options;
			this._MissingStatement = $"{this._Name} was not provided and is required.";
			this._DefaultValue = defaultValue;
			if (!String.IsNullOrWhiteSpace(this._DefaultValue))
				this._Value = this._DefaultValue;
			this._InvalidMessage = null;
			this._IsMissing = true;
			this._IsValid = false;
			this._IsProvided = false;
		} 

		public virtual ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = new();
			String argument = $"--{this._Name}";
			if (!String.IsNullOrWhiteSpace(this._Alias))
				argument += $", -{this._Alias}";
			//if (!this._IsRequired)
			//	argument = $"[{argument}]";
			returnValue.Add
			(
				this.IsRequired
					? ConsoleText.DarkRed($"{argument, -45}")
					: ConsoleText.DarkGreen($"{argument, -45}")
			);
			if (this._IsFlag)
				returnValue.Add(ConsoleText.White("[Flag] "));
			returnValue.Add(ConsoleText.White($"{this._Description}"));
			if (!String.IsNullOrEmpty(this._DefaultValue))
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.White($"{"",26}Default: {this._DefaultValue}"));
			}
			if (this._Options is not null && this._Options.Length > 0)
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.Blue($"{"",26}Options:"));
				foreach (ICommandArgumentOption option in this._Options)
				{
					returnValue.Add(ConsoleText.BlankLine());
					returnValue.Add(ConsoleText.Blue($"{"",29}{option.Value,-12}   {option.Description}"));
				}
			}
			return returnValue.ToArray();
		}

		public virtual void SetValue(String? value)
		{
			this._IsMissing = false;

			//this argument is a flag
			// If a flag is present, but no value is provided
			if (this._IsFlag)
			{
				//The value is not provided
				// if the flag is present and a value is not provided,
				//	Then true is assumed.
				if (String.IsNullOrEmpty(value))
				{
					this._IsProvided = true;
					this._IsValid = true;
					this._Value = "true";
				}

				//The value is provided
				else
				{
					//the value is a "True"
					if (
						value.ToLower() == "true"
						|| value.ToLower() == "yes"
						|| value.ToLower() == "1"
					)
					{
						this._IsValid = true;
						this._Value = "true";
					}

					//the value is a "False"
					else if (
						value.ToLower() == "false"
						|| value.ToLower() == "no"
						|| value.ToLower() == "0"
					)
					{
						this._IsValid = true;
						this._Value = "false";
					}

					//otherwise the value is invalid
					else
					{
						this._IsValid = false;
						this._InvalidMessage = $"{this._Name}: \"{value}\" is not an acceptable option. Must be true or false";
					}
				}
			}

			//this argument is option based
			else if (
				this._Options is not null
				&& this._Options.Length > 0
			)
			{
				//The value is not provided
				if (String.IsNullOrEmpty(value))
				{
					this._IsProvided = false;

					//It's also not required
					if (!this._IsRequired)
						this._IsValid = true;

					//It is required
					else
					{
						//but we DO NOT have a default value to use
						if (String.IsNullOrEmpty(this._DefaultValue))
						{
							this._IsValid = false;
							this._InvalidMessage = $"{this._Name}: one of the acceptable options must be provided.";
						}
						//we have a default value to use
						else
						{
							//the default value is a valid option
							if (this._Options.Any(o => o.Value.ToLower().Equals(this._DefaultValue.ToLower())))
							{
								this._IsValid = true;
								this._Value = this._DefaultValue;
							}
							//the default value is NOT a valid value.
							else
							{
								this._IsValid = false;
								this._InvalidMessage = $"{this._Name}: The default value is not an acceptable option.";
							}
						}
					}

				}

				//The value is provided
				else
				{
					//The option in value is a valid option
					if (this._Options.Any(o => o.Value.ToLower().Equals(value.ToLower())))
					{
						this._IsValid = true;
						this._Value = value;
					}
					//The option in value is NOT a valid option
					else
					{
						this._IsValid = false;
						this._InvalidMessage = $"{this._Name}: \"{value}\" is not an acceptable option.";
					}
				}
			}


			//the argument is a basic value type
			else
			{
				//The value is not provided
				if (String.IsNullOrEmpty(value))
				{
					this._IsProvided = false;

					//It's also not required
					if (!this._IsRequired)
						this._IsValid = true;

					//It is required
					else
					{
						//but we DO NOT have a default value to use
						if (String.IsNullOrEmpty(this._DefaultValue))
						{
							this._IsValid = false;
							this._InvalidMessage = $"{this._Name}: Value is required.";
						}
						//we have a default value to use
						else
						{
							this._IsValid = true;
							this._Value = this._DefaultValue;
						}
					}
				}

				//The value is provided
				else
				{
					this._IsValid = true;
					this._Value = value;
				}
			}
		}

		public virtual String GetValue()
		{
			return this._Value ?? "";
		}
	}
}
