using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
    public class CommandArgumentOptionBase : ICommandArgumentOption
	{
		public CommandArgumentOptionBase() { }

        public CommandArgumentOptionBase(String value, String description)
        {
            this.Value = value;
            this.Description = description;
        }

        public String Value { get; }

		public String Description { get; }
	}
}
