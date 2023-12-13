using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public interface ICommand
	{
		public String Name { get; }
		public String Description { get; }
		public String Usage { get; }
		public String Example { get; }
		public String[] Aliases { get; }
		public Boolean IsArgumentsValid { get; }
		public Dictionary<String, ICommandArgument> Arguments { get; }
		public Dictionary<String, String> ArgumentAliases { get; }

		public void VerifyArguments(Dictionary<String, String> arguments);
		public ConsoleText[] GetHelpText();
		public void Execute();
	}
}
