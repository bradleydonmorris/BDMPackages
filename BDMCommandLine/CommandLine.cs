using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

namespace BDMCommandLine
{
	public class CommandLine
	{
		#region Statics
		public static AssetVersions AssetVersions { get; set; } = [
			new AssetVersion()
			{
				Name = "BDMCommandLine",
				Version = "v1.6.4",
				Description = "Parses command line arguments for console app and provides simplified colorization for console text.",
				Copyright = "Copyright © 2021 Bradley Don Morris",
				InfoURL = "https://bradleydonmorris.me/packages/BDMCommandLine"
			}
		];

		public static ICommand? ActiveCommand { get; set; }

		public static Commands Commands { get; set; } = [];

		//public static Dictionary<String, ICommand> Commands { get; set; } = [];
		//public static Dictionary<String, String> CommandAliases { get; set; } = [];

		public static void AddCommand(ICommand command)
			=> CommandLine.Commands.Add(command);
		public static void AddCommands(params ICommand[] commands)
		{
			foreach (ICommand command in commands)
				CommandLine.AddCommand(command);
		}

		public static void ReplaceCommand(ICommand newCommand)
			=> CommandLine.Commands.Replace(newCommand);
		#endregion Statics

		#region Contructors
		public CommandLine(String defaultCommand)
		{
			CommandLine.Commands = [];
			this.DefaultCommand = defaultCommand;
			this.ParsedArguments = [];
			this.ActiveCommandArguments = [];
			this.ProvidedArguments = [];
			this.SubCommandProvided = String.Empty;
			this.IsActiveCommandValid = false;
			CommandLine.AddCommand(new HelpCommand());
			CommandLine.AddCommand(new VersionCommand());
		}

		public CommandLine()
			: this("help")
		{ }

		public CommandLine(String[]? args)
			: this("help")
			=> this.Parse(args);

		public CommandLine(String defaultCommand, ICommand[] commands)
			: this(defaultCommand)
		=> CommandLine.AddCommands(commands);

		public CommandLine(String defaultCommand, ICommand[] commands, String[]? args)
			: this(defaultCommand, commands)
			=> this.Parse(args);

		public CommandLine(ICommand[] commands)
			: this("help", commands)
		{ }

		public CommandLine(ICommand[] commands, String[]? args)
			: this("help", commands)
			=> this.Parse(args);
		#endregion Contructors

		public String DefaultCommand { get; set; }

		public Dictionary<String, String?> ParsedArguments { get; set; } = [];
		public Dictionary<String, String> ActiveCommandArguments { get; set; } = [];
		public Dictionary<String, String?> ProvidedArguments { get; set; } = [];

		public String SubCommandProvided { get; set; } = String.Empty;
		public String? SubCommand { get; set; }
		public Boolean IsActiveCommandValid { get; set; } = false;

		private void VerifyCommand()
		{
			if (this.SubCommand is not null)
			{
				CommandLine.ActiveCommand = CommandLine.Commands[this.SubCommand];
				if (CommandLine.ActiveCommand is not null)
				{
					if (CommandLine.ActiveCommand.Arguments is not null)
						CommandLine.ActiveCommand.VerifyArguments(this.ParsedArguments);
					this.IsActiveCommandValid = CommandLine.ActiveCommand.IsArgumentsValid;
				}
				else
					this.IsActiveCommandValid = false;
			}
			else
				this.IsActiveCommandValid = false;
		}


		public void Parse(String[]? arguments)
		{
			Boolean doExecute = true;

			//No args, then try default command
			if (
				(
					arguments is null
					|| arguments.Length == 0
				)
				&& this.DefaultCommand is not null
			)
				arguments = [ this.DefaultCommand ];

			//if "help", then try to mutate args to match help command
			if (
				arguments is not null
				&& arguments.Length > 0
				&& arguments[0].Equals("Help", StringComparison.CurrentCultureIgnoreCase)
			)
			{
				//Help requested for a specific command
				//If 2 or more args are passed, it's assumned that the second is a command name,
				// and all other args are ignored.
				if (arguments.Length.Equals(2))
					arguments = [ "Help", "-c", arguments[1] ];
			}

			//Establish command
			if (arguments is not null && arguments.Length > 0)
			{
				this.SubCommandProvided = arguments[0];
				if (
					CommandLine.Commands.TryGet(this.SubCommandProvided, out ICommand? command)
					&& command is not null
				)
				{
					this.SubCommand = command.Name;
					CommandLine.ActiveCommand = command;
				}
			}

			if (this.SubCommand is not null)
			{
				this.ProvidedArguments = [];
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
						else if (argument.StartsWith('-'))
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
								: key.StartsWith('-')
									? key[1..]
									: key
						), this.ProvidedArguments[key]);
				this.VerifyCommand();
			}

			//No command
			if (this.SubCommand is null)
			{
				CommandLine.OutputException("No command was provided.");
				doExecute = false;
			}

			//Command not found
			else if (
				this.SubCommand is not null
				&& CommandLine.ActiveCommand is null
			)
			{
				CommandLine.OutputException($"\"{this.SubCommandProvided}\" is not a valid command.");
				doExecute = false;
			}

			//Command is found, but arguments provided aren't valid
			else if (
				CommandLine.ActiveCommand is not null
				&& !this.IsActiveCommandValid
			)
			{
				String message = $"\"{this.SubCommandProvided}\" is a valid command however, there are arguments that may be invalid.\n";
				foreach (ICommandArgument argument in CommandLine.ActiveCommand.Arguments)
					if (!argument.IsValid)
						message += $"{argument.InvalidMessage}\n";

				//foreach (String argumentKey in CommandLine.ActiveCommand.Arguments.Keys)
				//	if (!CommandLine.ActiveCommand.Arguments[argumentKey].IsValid)
				//		message += $"{CommandLine.ActiveCommand.Arguments[argumentKey].InvalidMessage}\n";

				CommandLine.OutputException(message);
				CommandLine.OutputTextCollection(CommandLine.ActiveCommand.GetHelpText());
				doExecute = false;
			}
			if (doExecute)
				CommandLine.ActiveCommand?.Execute();
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
	}
}
