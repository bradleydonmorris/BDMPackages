using System;

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

        public String Value { get; set; } = String.Empty;

		public String Description { get; set; } = String.Empty;
	}
}
