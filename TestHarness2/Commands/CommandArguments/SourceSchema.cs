using BDMCommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands.CommandArguments
{
    internal class SourceSchema : CommandArgumentBase
    {
        public SourceSchema()
            : base(
				  "SourceSchema",
                  "srcSchema",
                  "Source schema",
                  false,
                  false,
                  null,
                  string.Empty)
        { }
    }
}
