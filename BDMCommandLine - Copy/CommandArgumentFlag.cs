using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public class CommandArgumentFlag : CommandArgumentBase
	{
		public CommandArgumentFlag
		(
			String name,
			String alias,
			String description
		)
			: base (
				name,
				alias,
				description,
				false, //Flags are never required
				true, //Yes it is a flag
				null, //Has no need for options
				"", //Missing statment is not needed as they are never seen as "missing".
				"true" //Default value is assumed to be true if the flag is present, and false if it's not
			)
		{
		}
	}
}
