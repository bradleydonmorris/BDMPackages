using BDMCommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands.CommandArguments
{
    internal class SourceTable : CommandArgumentBase
    {
        public SourceTable()
            : base(
				  "SourceTable",
				  "srcTable",
				  "Source table",
				  true,
                  false,
                  null,
                  string.Empty)
        { }
    }
}
