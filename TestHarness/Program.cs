using BDMJsonProcs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

using Serilog;
using Serilog.Events;
using BDMSerilogProc;

namespace TestHarness
{
	class Program
	{
		static void Main() //(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.BDMSerilogProc(
					"Server=localhost;Initial Catalog=AAONEnterprise;User Id=AAONEnterprise.Logger;Password=cf$g0f3FMxvkuwedSD#@IbHd162Vrm*5#SE;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=SerilogPoC;",
					"Logging",
					"Enqueue",
					"JSON",
					LogEventLevel.Verbose
				).CreateLogger();
			try
			{
				throw new Exception("Gettin it goin.");
			}
			catch (Exception exception)
			{
				Log.Logger.Error(exception, "{CallingMethod}", new CallingMethod()
				{
					ClassName = typeof(Program).FullName,
					Name = System.Reflection.MethodBase.GetCurrentMethod()?.Name
				}
				);
			}


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
