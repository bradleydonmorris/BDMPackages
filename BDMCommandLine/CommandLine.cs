using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public class CommandLine
	{
		public CommandLine()
		{
			this.Arguments = new();
			this.Commands = new();
			this.CommandAliases = new();
		}

		public ICommand ActiveCommand { get; set; }

		public Dictionary<String, String> Arguments { get; set; }
		public Dictionary<String, ICommand> Commands { get; set; }
		public Dictionary<String, String> CommandAliases { get; set; }

		public String SubCommand { get; set; }
		public Boolean IsVerified { get; set; }

		public void ShowHelp()
		{
			if (this.IsVerified)
				CommandLine.OutputTextCollection(this.Commands[this.SubCommand].GetHelpText());
			else CommandLine.OutputTextCollection(this.GetHelpText());
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

		private String[] VerifyCommand()
		{
			List<String> returnValue = new();
			if (this.SubCommand.ToLower() == "invalid")
				returnValue.Add("Command is invalid");
			else
			{
				this.ActiveCommand = this.Commands[this.SubCommand];
				if (this.ActiveCommand.Arguments is not null)
					returnValue.AddRange(this.ActiveCommand.VerifyArguments(this.Arguments));
			}
			if (returnValue.Count == 0)
				this.IsVerified = true;
			else
				this.IsVerified = false;
			return returnValue.ToArray();
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

		public Boolean ParseArguments(String[] arguments)
		{
			if (arguments.Length > 0)
			{
				String subCommand = arguments[0].ToLower();
				this.SubCommand = "invalid";
				if (this.CommandAliases.ContainsKey(subCommand))
					this.SubCommand = this.CommandAliases[subCommand];
				if (String.IsNullOrWhiteSpace(this.SubCommand))
					this.SubCommand = "invalid";
			}
			Dictionary<String, String> parsingArguments = new();
			if (arguments.Length > 1)
				for (Int32 loop = 1; loop < arguments.Length; loop++)
				{
					String argument = arguments[loop];
					String previous = null;
					if (loop > 1)
						previous = arguments[loop - 1];
					if (argument.StartsWith("--"))
						parsingArguments.Add(argument, null);
					else if (argument.StartsWith("-"))
						parsingArguments.Add(argument, null);
					else if (!String.IsNullOrWhiteSpace(previous) && parsingArguments.ContainsKey(previous))
						parsingArguments[previous] = argument;
					else
						parsingArguments.Add(argument, argument);
				}
			foreach (String key in parsingArguments.Keys)
			{
				String value = String.IsNullOrWhiteSpace(parsingArguments[key])
					? "true"
					: parsingArguments[key];
				if (key.StartsWith("--"))
					this.Arguments.Add(key[2..], value);
				else if (key.StartsWith("-"))
					this.Arguments.Add(key[1..], value);
				else
					this.Arguments.Add(key, value);
			}

			String[] results = this.VerifyCommand();
			if (results.Length > 0)
			{
				foreach (String result in results)
					CommandLine.OutputException(result);
				this.ShowHelp();
			}
			return (results.Length == 0);
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

		public static String ReadLine()
        {
			return System.Console.ReadLine();
        }

		public static void Clear()
		{
			System.Console.Clear();
		}

		public void Execute()
		{
			this.ActiveCommand.Execute();
		}
	}
}
