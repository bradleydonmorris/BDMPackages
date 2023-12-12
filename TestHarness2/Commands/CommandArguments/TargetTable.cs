using BDMCommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands.CommandArguments
{
    internal class TargetTable : CommandArgumentBase
    {
        public TargetTable()
            : base(
				  "TargetTable",
				  "trgTable",
				  "Target table",
				  true,
                  false,
                  null,
                  string.Empty)
        { }
    }
}
