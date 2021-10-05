using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public interface ICommand
	{
		public void Execute();
		String[] VerifyArguments(CommandArgument[] commandArguments);
		ConsoleText[] GetHelpText();

		public String Name { get; }
		public String[] Aliases { get; }
		public CommandArgument[] Arguments { get; set; }
		public String Description { get; }
		public String Usage { get; }
	}
}
