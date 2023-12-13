#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public class CommandLine
	{
		public CommandLine(String defaultCommand)
		{
			this.ParsedArguments = new();
			this.ActiveCommandArguments = new();
			this.ProvidedArguments = new();
			this.Commands = new();
			this.CommandAliases = new();
			this.DefaultCommand = defaultCommand;
			this.IsActiveCommandValid = false;
		}
		public CommandLine(String defaultCommand, params ICommand[] commands)
			: this(defaultCommand)
		=> this.AddCommands(commands);

		public CommandLine()
			: this("help")
		{ }

		public CommandLine(params ICommand[] commands)
			: this("help", commands)
		{ }

		public String DefaultCommand { get; set; }

		public ICommand? ActiveCommand { get; set; }

		public Dictionary<String, String?> ParsedArguments { get; set; }
		public Dictionary<String, String> ActiveCommandArguments { get; set; }
		public Dictionary<String, String?> ProvidedArguments { get; set; }

		public Dictionary<String, ICommand> Commands { get; set; }
		public Dictionary<String, String> CommandAliases { get; set; }

		public String SubCommandProvided { get; set; } = String.Empty;
		public String? SubCommand { get; set; }
		public Boolean IsActiveCommandValid { get; set; }

		public void ShowHelp(String? command)
		{
			if (command is not null)
				CommandLine.OutputTextCollection(this.Commands[command].GetHelpText());
			else
				CommandLine.OutputTextCollection(this.GetHelpText());
		}

		public ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = new();
			Boolean isFirst = true;
			foreach (String key in this.Commands.Keys)
			{
				if (!isFirst)
					returnValue.Add(ConsoleText.BlankLine());
				else isFirst = false;
				returnValue.AddRange(this.Commands[key].GetHelpText());
			}
			returnValue.Add(ConsoleText.BlankLine());
			return returnValue.ToArray();
		}

		private void VerifyCommand()
		{
			if (this.SubCommand is not null)
			{
				this.ActiveCommand = this.Commands[this.SubCommand];
				if (this.ActiveCommand is not null)
				{
					if (this.ActiveCommand.Arguments is not null)
						this.ActiveCommand.VerifyArguments(this.ParsedArguments);
					this.IsActiveCommandValid = this.ActiveCommand.IsArgumentsValid;
				}
				else
					this.IsActiveCommandValid = false;
			}
			else
				this.IsActiveCommandValid = false;
		}

		public void AddCommand(ICommand command)
		{
			String commandNameLower = command.Name.ToLower();
			this.Commands.Add(commandNameLower, command);
			foreach (String alias in command.Aliases)
				this.CommandAliases.Add(alias.ToLower(), commandNameLower);
			if (!this.CommandAliases.ContainsKey(commandNameLower))
				this.CommandAliases.Add(commandNameLower, commandNameLower);
		}

		public void AddCommands(params ICommand[] commands)
		{
			foreach (ICommand command in commands)
				this.AddCommand(command);
		}

		public Boolean ParseArguments(String[]? arguments)
		{
			Boolean returnValue = true;

			//No command provided
			if (
				(
					arguments is null
					|| arguments.Length == 0
				)
				&& this.DefaultCommand is not null
			)
				this.SubCommandProvided = this.DefaultCommand;

			//Help requested
			else if (
				arguments is not null
				&& arguments.Length > 0
				&& arguments[0].ToLower().Equals("help")
			)
			{
				this.SubCommandProvided = "help";
				//Help requested for a specific command
				if (arguments.Length > 1)
					if (this.CommandAliases.ContainsKey(arguments[1].ToLower()))
						this.SubCommand = this.CommandAliases[arguments[1].ToLower()];
			}

			//Not Help
			else if (arguments is not null && arguments.Length > 0)
			{
				this.SubCommandProvided = arguments[0];
				if (this.CommandAliases.ContainsKey(this.SubCommandProvided.ToLower()))
					this.SubCommand = this.CommandAliases[this.SubCommandProvided.ToLower()];
			}

			//Not a help command
			if (
				this.SubCommand is not null
				&& !this.SubCommand.ToLower().Equals("help")
			)
			{
				this.ProvidedArguments = new();
				if (
					arguments is not null
					&& arguments.Length > 1
				)
					for (Int32 loop = 1; loop < arguments.Length; loop++)
					{
						String argument = arguments[loop];
						String previous = String.Empty;
						if (loop > 1)
							previous = arguments[loop - 1];
						if (argument.StartsWith("--"))
							this.ProvidedArguments.Add(argument, null);
						else if (argument.StartsWith("-"))
							this.ProvidedArguments.Add(argument, null);
						else if (
							!String.IsNullOrWhiteSpace(previous)
							&& this.ProvidedArguments.ContainsKey(previous)
						)
							this.ProvidedArguments[previous] = argument;
						else
							this.ProvidedArguments.Add(argument, argument);
					}
				foreach (String key in this.ProvidedArguments.Keys)
					this.ParsedArguments.Add(
						(
							key.StartsWith("--")
								? key[2..]
								: key.StartsWith("-")
									? key[1..]
									: key
						), this.ProvidedArguments[key]);
				this.VerifyCommand();
			}

			//No command
			if (this.SubCommand is null)
			{
				CommandLine.OutputException("No command was provided.");
				returnValue = false;
			}

			//Help command
			else if (
				this.SubCommand is not null
				&& this.SubCommand.ToLower().Equals("help")
			)
			{

			}

			//Command not found
			else if (
				this.SubCommand is not null
				&& this.ActiveCommand is null
			)
			{
				CommandLine.OutputException($"\"{this.SubCommandProvided}\" is not a valid command.");
				returnValue = false;
			}

			//Command isn't Help, but is valid.
			//	However, no valid arguments were provided.=
			else if (
				this.SubCommand is not null
				&& this.SubCommand.ToLower().Equals("help")

				&& this.ActiveCommand is not null
				&& !this.IsActiveCommandValid
			)
			{
				String message = $"\"{this.SubCommandProvided}\" is a valid command however, there are arguments that may be invalid.\n";
				foreach (String argumentKey in this.ActiveCommand.Arguments.Keys)
					if (!this.ActiveCommand.Arguments[argumentKey].IsValid)
						message += $"{this.ActiveCommand.Arguments[argumentKey].InvalidMessage}\n";
				CommandLine.OutputException(message);
				returnValue = false;
			}
			return returnValue;
		}

		public static void OutputException(String message)
		{
			CommandLine.OutputTextCollection(
				ConsoleText.Red("\n*****************************************\n"),
				ConsoleText.Red(message),
				ConsoleText.Red("\n*****************************************\n"),
				ConsoleText.BlankLine()
			);
		}

		public static void OutputException(Exception exception)
		{
			CommandLine.OutputException(exception.ToString());
		}

		public static void OutputTextCollection(params ConsoleText[] texts)
		{
			foreach (ConsoleText text in texts)
			{
				System.Console.ResetColor();
				System.Console.ForegroundColor = text.ForegroundColor;
				System.Console.BackgroundColor = text.BackgroundColor;
				System.Console.Write(text.Text);
				System.Console.ResetColor();
			}
		}

		public static void OutputTextCollection(List<ConsoleText> texts)
		{
			foreach (ConsoleText text in texts)
			{
				System.Console.ResetColor();
				System.Console.ForegroundColor = text.ForegroundColor;
				System.Console.BackgroundColor = text.BackgroundColor;
				System.Console.Write(text.Text);
				System.Console.ResetColor();
			}
		}

		public static String? ReadLine()
        {
			return System.Console.ReadLine();
        }

		public static void Clear()
		{
			System.Console.Clear();
		}

		public void Execute()
			=> this.ActiveCommand?.Execute();
	}
}
