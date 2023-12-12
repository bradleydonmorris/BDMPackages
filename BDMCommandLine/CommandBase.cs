using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public class CommandBase : ICommand
	{
		public CommandBase() { }

		public CommandBase(String name, String description, String usage, String example, String[] aliases, ICommandArgument[] arguments)
		{
			this._Name = name;
			this._Description = description;
			this._Usage = usage;
			this._Example = example;
			this._Aliases = aliases;
			this._Arguments = new();
			this._ArgumentAliases = new();
			foreach (ICommandArgument argument in arguments)
			{
				String argumentNameLower = argument.Name.ToLower();
				this._Arguments.Add(argumentNameLower, argument);
				if (!String.IsNullOrWhiteSpace(argument.Alias))
					this._ArgumentAliases.Add(argument.Alias.ToLower(), argumentNameLower);
				if (!this._ArgumentAliases.ContainsKey(argumentNameLower))
					this._ArgumentAliases.Add(argumentNameLower, argumentNameLower);
			}
		}


		private readonly String _Name;
		public virtual String Name => this._Name;

		private readonly String _Description;
		public virtual String Description => this._Usage;

		private readonly String _Usage;
		public virtual String Usage => this._Usage;

		private readonly String _Example;
		public virtual String Example => this._Example;

		private readonly String[] _Aliases;
		public virtual String[] Aliases => this._Aliases;

		public Boolean _IsArgumentsValid;
		public Boolean IsArgumentsValid => this._IsArgumentsValid;


		private readonly Dictionary<String, ICommandArgument> _Arguments;
		public virtual Dictionary<String, ICommandArgument> Arguments => this._Arguments;

		private readonly Dictionary<String, String> _ArgumentAliases;
		public Dictionary<String, String> ArgumentAliases => this._ArgumentAliases;


		public virtual void VerifyArguments(Dictionary<String, String> arguments)
		{
			this._IsArgumentsValid = true;
			if (this._Arguments is not null && this._Arguments.Count > 0)
			{
				foreach (String providedArgumentKey in arguments.Keys)
				{
					if (this._ArgumentAliases.ContainsKey(providedArgumentKey.ToLower()))
					{
						String foundArgumentKey = this._ArgumentAliases[providedArgumentKey.ToLower()];
						if (
							!String.IsNullOrEmpty(foundArgumentKey)
							&& this._Arguments.ContainsKey(foundArgumentKey)
						)
						{
							this._Arguments[foundArgumentKey.ToLower()].SetValue(arguments[providedArgumentKey]);
						}
					}
				}
			}
			foreach (String argumentKey in this._Arguments.Keys)
			{
				if (
					this._Arguments[argumentKey].IsMissing
					&& this._Arguments[argumentKey].IsFlag
				)
					this._Arguments[argumentKey].SetValue(this._Arguments[argumentKey].DefaultValue);
				if (
					this._Arguments[argumentKey].IsMissing
					&& this._Arguments[argumentKey].IsRequired

				)
					this._Arguments[argumentKey].SetValue(this._Arguments[argumentKey].DefaultValue);
				if (!this._Arguments[argumentKey].IsValid)
					this._IsArgumentsValid = false;
			}
		}

		public virtual ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = new()
			{
				ConsoleText.Green($"Command: {this._Name}")
			};
			if (this.Aliases.Length > 0)
			{
				returnValue.Add(ConsoleText.Green($" [ {String.Join(", ", this.Aliases)} ]"));
			}
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.White($"   {this._Description}"));
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.White($"   Usage: {this._Usage}"));
			returnValue.Add(ConsoleText.BlankLine());
			if (this._Arguments is not null && this._Arguments.Count > 0)
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.White("   Arguments: ("));
				returnValue.Add(ConsoleText.DarkRed("required"));
				returnValue.Add(ConsoleText.DarkRed(", "));
				returnValue.Add(ConsoleText.DarkGreen("optional"));
				returnValue.Add(ConsoleText.White(")"));
				foreach (String argumentKey in this._Arguments.Keys)
				{
					returnValue.Add(ConsoleText.BlankLine());
					returnValue.Add(ConsoleText.Default($"   "));
					returnValue.AddRange(this._Arguments[argumentKey].GetHelpText());
				}
			}
			returnValue.Add(ConsoleText.BlankLine());

			if (!String.IsNullOrEmpty(this._Example))
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.White($"   Example:"));
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.Green($"   {this._Example}"));
				returnValue.Add(ConsoleText.BlankLine());
			}
			return returnValue.ToArray();
		}

		public virtual void Execute()
		{
			throw new NotImplementedException();
		}
	}
}