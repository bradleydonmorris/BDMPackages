using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
    public class LoggingLevelArgument : CommandArgumentBase
    {
		public LoggingLevelArgument()
			: base
			(
				name: "LogLevel",
				alias: "log",
				description: "Level of logging",
				isRequired: false,
				isFlag: false,
				options: new ICommandArgumentOption[]
				{
					new CommandArgumentOptionBase("Verbose", "Anything and everything you might want to know about a running block of code."),
					new CommandArgumentOptionBase("Debug", "Internal system events that aren't necessarily observable from the outside."),
					new CommandArgumentOptionBase("Information", "The lifeblood of operational intelligence - things happen."),
					new CommandArgumentOptionBase("Warning", "Service is degraded or endangered."),
					new CommandArgumentOptionBase("Error", "Functionality is unavailable, invariants are broken or data is lost."),
					new CommandArgumentOptionBase("Fatal", "If you have a pager, it goes off when one of these occurs.")
				},
				defaultValue: null
			)
		{ }
		public LoggingLevelArgument(String level)
		: base
		(
			name: "LogLevel",
			alias: "log",
			description: "Level of logging",
			isRequired: false,
			isFlag: false,
			options: new ICommandArgumentOption[]
			{
					new CommandArgumentOptionBase("Verbose", "Anything and everything you might want to know about a running block of code."),
					new CommandArgumentOptionBase("Debug", "Internal system events that aren't necessarily observable from the outside."),
					new CommandArgumentOptionBase("Information", "The lifeblood of operational intelligence - things happen."),
					new CommandArgumentOptionBase("Warning", "Service is degraded or endangered."),
					new CommandArgumentOptionBase("Error", "Functionality is unavailable, invariants are broken or data is lost."),
					new CommandArgumentOptionBase("Fatal", "If you have a pager, it goes off when one of these occurs.")
			},
			defaultValue: level
		)
		{ }
	}
}
