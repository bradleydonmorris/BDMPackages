using BDMJsonProcs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TestHarness
{
	class Program
	{
		static void Main(string[] args)
		{
			String connectionString = "Server=localhost;Initial Catalog=BDMSQLUtilities;User Id=BDMSQLUtilities.WebAPI;Password=213SDfg34sdabghfd&##dsfgsdfg^;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"BDMSQLUtilities.WebAPI\";";

			using (SqlCommand sqlCommand = new())
			{
				var sdfasdf = sqlCommand.ExecuteScalarJsonProc<List<String>>(
					$"[Optimization].[GetMissingIndexStatistics.json]",
					connectionString,
					new SqlParameter() { ParameterName = "@DatabaseName", SqlDbType = SqlDbType.NVarChar, Size = 128, SqlValue = "TrashDB", Direction = ParameterDirection.Input }
				);
			}
			Console.WriteLine("Hello World!");
		}
	}
}
