using System;
using System.Collections.Generic;

namespace BDMCommandLine
{
	public interface ICommand
	{
		public String Name { get; set; }
		public String Description { get; set; }
		public String Usage { get; set; }
		public String Example { get; set; }
		public String[] Aliases { get; set; }
		public Boolean IsArgumentsValid { get; set; }
		public Arguments Arguments { get; set; }

		public void VerifyArguments(Dictionary<String, String?> arguments);
		public ConsoleText[] GetHelpText();
		public void Execute();
	}
}
