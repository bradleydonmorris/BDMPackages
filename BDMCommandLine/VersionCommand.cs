using System;
using System.Collections.Generic;
using System.Reflection;

namespace BDMCommandLine
{
	public class VersionCommand : CommandBase
	{
        public const String CommandName = "Version";
        public VersionCommand()
            : base(
				VersionCommand.CommandName,
                "Used to display version information.",
				"Version",
                "{EXEPath} version",
                ["version"],
				[]
		)
		{ }

		public override void Execute()
        {
			List<ConsoleText> consoleTexts = [];
			foreach (AssetVersion assetVersion in CommandLine.AssetVersions)
			{
				consoleTexts.AddRange([
					ConsoleText.Default($"{assetVersion.Name} {assetVersion.Version}"),
				ConsoleText.BlankLine(),
				ConsoleText.DarkGreen($"  {assetVersion.Description}"),
				ConsoleText.BlankLine(),
				ConsoleText.DarkGreen($"  {assetVersion.Copyright.Replace("©", "(c)")}"),
					ConsoleText.BlankLine(),
					ConsoleText.DarkGreen($"  {assetVersion.InfoURL}"),
					ConsoleText.BlankLines(2)
				]);
			}
			CommandLine.OutputTextCollection(consoleTexts);
		}
	}
}
