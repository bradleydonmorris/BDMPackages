using System;
using System.Collections.Generic;

namespace BDMCommandLine
{
	public class HelpCommand : CommandBase
	{
        public const String CommandName = "Help";
        public HelpCommand()
            : base(
                HelpCommand.CommandName,
                "Used to display help.",
				"Help [command]",
                "{EXEPath} help [command]",
                ["help"],
				new CommandArgumentBase[] {
					CommandArgumentBase.CreateSimpleArgument("Command", "c", "Command to get help on.", false)
				}
		)
		{ }

		public override ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = [];
			Boolean isFirst = true;
			returnValue.AddRange([
				ConsoleText.Default("Available Commands:"),
				ConsoleText.BlankLine()
			]);
			foreach (ICommand command in CommandLine.Commands)
			{
				if (!command.Name.Equals(HelpCommand.CommandName, StringComparison.InvariantCultureIgnoreCase))
				{
					if (!isFirst)
						returnValue.Add(ConsoleText.BlankLine());
					else isFirst = false;
					returnValue.AddRange([
						ConsoleText.Green($"   {command.Name}{(
							this.Aliases.Length > 0
								? $" [ {String.Join(", ", command.Aliases)} ]"
								: ""
						)}"),
						ConsoleText.White($"   {command.Description}")
					]);
				}
			}
			returnValue.Add(ConsoleText.BlankLine());
			return [.. returnValue];
		}

		public override void Execute()
        {
			if (CommandLine.Commands.TryGet(base.Arguments.GetSimpleValue("Command"), out ICommand? command)
				&& command is not null
			)
				CommandLine.OutputTextCollection(command.GetHelpText());
			else
				CommandLine.OutputTextCollection(this.GetHelpText());
        }
    }
}
