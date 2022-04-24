using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
    public interface ICommandArgument
    {
		public String Name { get; }
		public String Description { get; }
		public String Alias { get; }
		public Boolean IsRequired { get; }
		public Boolean IsFlag { get; }
		public ICommandArgumentOption[] Options { get; }

		public Boolean IsVerified { get; }
		public Boolean IsProvided { get; }
		public String Value { get; }
		public String DefaultValue { get; }
		public Boolean IsFlagedTrue { get; }

		public ConsoleText[] GetHelpText();
		public String SetValue(String value);
		public String GetValue();
	}
}
