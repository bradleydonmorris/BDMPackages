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

		public CommandBase(String name, String description, String usage, String[] aliases, ICommandArgument[] arguments)
		{
			this._Name = name;
			this._Description = description;
			this._Usage = usage;
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

		private readonly String[] _Aliases;
		public virtual String[] Aliases => this._Aliases;


		private readonly Dictionary<String, ICommandArgument> _Arguments;
		public virtual Dictionary<String, ICommandArgument> Arguments => this._Arguments;

		private readonly Dictionary<String, String> _ArgumentAliases;
		public Dictionary<String, String> ArgumentAliases => this._ArgumentAliases;


		public virtual String[] VerifyArguments(Dictionary<String, String> arguments)
		{
			List<String> returnValue = new();
			if (this._Arguments != null && this._Arguments.Count > 0)
			{
				foreach (String providedArgumentKey in arguments.Keys)
				{
					String foundArgumentKey = this._ArgumentAliases[providedArgumentKey];
					if (
						!String.IsNullOrEmpty(foundArgumentKey)
						&& this._Arguments.ContainsKey(foundArgumentKey)
					)
                    {
						String result = this._Arguments[foundArgumentKey].SetValue(arguments[providedArgumentKey]);
						if (!String.IsNullOrEmpty(result))
							returnValue.Add(result);
					}
				}
			}
			return returnValue.ToArray();
		}

		public virtual ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = new();
			returnValue.Add(ConsoleText.Green($"Command: {this.Name}"));
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.White($"   {this._Description}"));
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.White($"   Usage: {this.Usage}"));
			if (this._Arguments != null && this._Arguments.Count > 0)
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.White("   Arguments: ("));
				returnValue.Add(ConsoleText.DarkRed("Dark Red = required"));
				returnValue.Add(ConsoleText.DarkRed(", "));
				returnValue.Add(ConsoleText.DarkGreen("Dark Green = optional"));
				returnValue.Add(ConsoleText.White(")"));
				foreach (String argumentKey in this._Arguments.Keys)
				{
					returnValue.Add(ConsoleText.BlankLine());
					returnValue.Add(ConsoleText.Default($"   "));
					returnValue.AddRange(this._Arguments[argumentKey].GetHelpText());
				}
			}
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.BlankLine());
			return returnValue.ToArray();
		}

		public virtual void Execute()
		{
			throw new NotImplementedException();
		}
	}
}