using BDMCommandLine;
using SQLServerPostgreSQLInterface.Commands.CommandArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerPostgreSQLInterface.Commands
{
    internal class SQLServer2PostgreSQL : CommandBase
    {
        public SQLServer2PostgreSQL()
            : base(
                "SQLServer2PostgreSQL",
                "Used to copy data from a SQL Server Table or View to a PostgreSQL Table.",
				"SQLServer2PostgreSQL [ss2pg] {arguments}",
				"SQLServerPostgreSQLInterface ss2pg -ssConn \"{mySQLServerConnString}\" -pgConn \"{myPostgreSQLConnString}\" -srcSchema \"dbo\" -srcTable \"MyTable\" -trgSchema \"public\" -trgTable \"myTarget\"",
				new string[] { "ss2pg" },
                new CommandArgumentBase[] {
                    new SQLServerConnectionString(),
					new PostgreSQLConnectionString(),
                    new SourceSchema(),
                    new SourceTable(),
                    new TargetSchema(),
                    new TargetTable()
				}
            )
        {
        }

        public override void Execute()
        {
			CommandLine.OutputTextCollection(ConsoleText.Blue("This is what I saw..."), ConsoleText.BlankLines(2));
			CommandLine.OutputTextCollection(
					ConsoleText.Default("Command: "),
					ConsoleText.Red(this.Name),
					ConsoleText.BlankLine()
			);
			foreach (String argumentKey in this.Arguments.Keys)
            {
				CommandLine.OutputTextCollection(
					ConsoleText.Default("    "),
					ConsoleText.Yellow(this.Arguments[argumentKey].Name),
					ConsoleText.Gray(" = "),
					ConsoleText.Red(this.Arguments[argumentKey].Value),
                    ConsoleText.BlankLine()
                );
			}
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
