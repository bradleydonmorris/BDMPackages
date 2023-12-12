using BDMCommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands.CommandArguments
{
    internal class TargetSchema : CommandArgumentBase
    {
        public TargetSchema()
            : base(
				  "TargetSchema",
                  "trgSchema",
                  "Target schema",
                  true,
                  false,
                  null,
                  string.Empty)
        { }
    }
}
