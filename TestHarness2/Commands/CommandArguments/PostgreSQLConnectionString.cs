using BDMCommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands.CommandArguments
{
    internal class PostgreSQLConnectionString : CommandArgumentBase
    {
        public PostgreSQLConnectionString()
            : base(
				  "PostgreSQLConnectionString",
                  "pgConn",
                  "Connection string for PostgreSQL",
                  true,
                  false,
                  null,
				  string.Empty)
        { }
    }
}
