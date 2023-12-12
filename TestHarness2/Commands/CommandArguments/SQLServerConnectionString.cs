using BDMCommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands.CommandArguments
{
    internal class SQLServerConnectionString : CommandArgumentBase
    {
        public SQLServerConnectionString()
            : base(
                  "SQLServerConnectionString",
                  "ssConn",
                  "Connection string for SQL Server",
                  true,
                  false,
                  null,
                  string.Empty)
        { }
    }
}
