using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		private readonly ICommandArgumentOption[] _Options;
		public ICommandArgumentOption[] Options => this._Options;


		private Boolean _IsVerified;
		public Boolean IsVerified => this._IsVerified;

		private Boolean _IsProvided;
		public Boolean IsProvided => this._IsProvided;

		private String _Value;
		public String Value => this._Value;

		private readonly String _DefaultValue;
		public String DefaultValue => this._DefaultValue;

		public Boolean IsFlagedTrue => (this.IsFlag && this.IsProvided);

		public CommandArgumentBase()
		{
			this._IsVerified = false;
			this._IsProvided = false;
			this._IsFlag = false;
		}

		public CommandArgumentBase(String name, String alias, String description, Boolean isRequired, Boolean isFlag, ICommandArgumentOption[] options, String defaultValue)
		{
			this._Name = name;
			this._Alias = alias;
			this._Description = description;
			this._IsRequired = isRequired;
			this._IsFlag = isFlag;
			this._Options = options;
			this._DefaultValue = defaultValue;
			if (!String.IsNullOrWhiteSpace(this._DefaultValue))
				this._Value = this._DefaultValue;
			this._IsVerified = false;
			this._IsProvided = false;
		}

		public virtual ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = new();
			String argument = $"--{this._Name}";
			if (!String.IsNullOrWhiteSpace(this._Alias))
				argument += $"|-{this._Alias}";
			if (!this._IsRequired)
				argument = $"[{argument}]";
			returnValue.Add
			(
				this.IsRequired
					? ConsoleText.DarkRed($"{argument,-20}")
					: ConsoleText.DarkGreen($"{argument,-20}")
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

		public virtual String SetValue(String value)
		{
			String returnValue = null;
			if (
				String.IsNullOrWhiteSpace(value)
				&& this._IsRequired
			)
			{
				this._IsVerified = false;
				returnValue = $"{this._Name}: Value is required.";
			}
			if (this.Options is not null && this._Options.Length > 0)
			{
				if (this._Options.Any(o => o.Value.ToLower().Equals(value.ToLower())))
				{
					this._IsProvided = true;
					this._IsVerified = true;
					this._Value = value;
				}
				else
				{
					this._IsProvided = false;
					this._IsVerified = false;
					returnValue = $"{this._Name}: \"{value}\" is not an acceptable option.";
				}
			}
			else if (
				this._IsFlag
				&&
				(
					String.IsNullOrWhiteSpace(value)
					|| value.ToLower() == "true"
				)
			)
			{
				this._IsProvided = true;
				this._IsVerified = true;
				this._Value = value;
			}
			return returnValue;
		}

		public virtual String GetValue()
		{
			return this._Value ?? "";
		}
	}
}
