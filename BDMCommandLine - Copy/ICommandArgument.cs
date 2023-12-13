#nullable enable
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
		public ICommandArgumentOption[]? Options { get; }
		public String MissingStatement { get; }

		public Boolean IsMissing { get; }
		public Boolean IsValid { get; }
		public Boolean IsProvided { get; }
		public String? Value { get; }
		public String? DefaultValue { get; }
		public String? InvalidMessage { get; }
		public Boolean IsFlagedTrue { get; }

		public ConsoleText[] GetHelpText();
		public void SetValue(String value);
		public String GetValue();
	}
}

