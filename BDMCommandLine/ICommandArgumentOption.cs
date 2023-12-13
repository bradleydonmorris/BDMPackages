using System;

namespace BDMCommandLine
{
	public interface ICommandArgumentOption
	{
		public String Value { get; set; }
		public String Description { get; set; }
	}
}
