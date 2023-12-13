using System;
using System.Collections.Generic;

namespace BDMCommandLine
{
	public class CommandBase : ICommand
	{
		public CommandBase() { }

		public CommandBase(String name, String description, String usage, String example, String[] aliases, ICommandArgument[] arguments)
		{
			this.Name = name;
			this.Description = description;
			this.Usage = usage;
			this.Example = example;
			this.Aliases = aliases;
			this.Arguments.AddRange(arguments);
		}

		public virtual String Name { get; set; } = String.Empty;
		public virtual String Description { get; set; } = String.Empty;
		public virtual String Usage { get; set; } = String.Empty;
		public virtual String Example { get; set; } = String.Empty;
		public virtual String[] Aliases { get; set; } = [];
		public Boolean IsArgumentsValid { get; set; } = false;
		public virtual Arguments Arguments { get; set; } = [];


		public virtual void VerifyArguments(Dictionary<String, String?> arguments)
		{
			this.IsArgumentsValid = true;
			if (this.Arguments.Count > 0)
			{
				foreach (String providedArgumentKey in arguments.Keys)
					if (this.Arguments.Contains(providedArgumentKey))
						this.Arguments[providedArgumentKey]?.SetValue(arguments[providedArgumentKey]);
			}
			foreach (ICommandArgument commandArgument in this.Arguments)
			{
				if (commandArgument.IsMissing)
					commandArgument.SetValue(commandArgument.DefaultValue);
				if (
					!commandArgument.IsValid
					&& commandArgument.IsRequired
				)
					this.IsArgumentsValid = false;
			}
		}

		public virtual ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = [ ConsoleText.Green($"Command: {this.Name}") ];
			if (this.Aliases.Length > 0)
			{
				returnValue.Add(ConsoleText.Green($" [ {String.Join(", ", this.Aliases)} ]"));
			}
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.White($"   {this.Description}"));
			returnValue.Add(ConsoleText.BlankLine());
			returnValue.Add(ConsoleText.White($"   Usage: {this.Usage}"));
			returnValue.Add(ConsoleText.BlankLine());
			if (this.Arguments.Count > 0)
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.White("   Arguments: ("));
				returnValue.Add(ConsoleText.DarkRed("required"));
				returnValue.Add(ConsoleText.DarkRed(", "));
				returnValue.Add(ConsoleText.DarkGreen("optional"));
				returnValue.Add(ConsoleText.White(")"));
				foreach (ICommandArgument commandArgument in this.Arguments)
				{
					returnValue.Add(ConsoleText.BlankLine());
					returnValue.Add(ConsoleText.Default($"   "));
					returnValue.AddRange(commandArgument.GetHelpText());
				}
			}
			returnValue.Add(ConsoleText.BlankLine());

			if (!String.IsNullOrEmpty(this.Example))
			{
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.White($"   Example:"));
				returnValue.Add(ConsoleText.BlankLine());
				returnValue.Add(ConsoleText.Green($"   {this.Example}"));
				returnValue.Add(ConsoleText.BlankLine());
			}
			return [.. returnValue];
		}

		public virtual void Execute()
			=> throw new NotImplementedException();
	}
}