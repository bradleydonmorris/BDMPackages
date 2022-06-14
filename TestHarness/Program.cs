using BDMJsonProcs;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

using Serilog;
using Serilog.Events;
using BDMSerilogProc;
using BDMEntityNumbering;

using SARTeam.Models;

namespace TestHarness
{
	class Program
	{
		static void Main() //(string[] args)
		{
			String connectionString = "Server=tcp:sarteam.database.windows.net,1433;Initial Catalog=SARTeamCore;Persist Security Info=False;User ID=API;Password=2caIz49u$d61r%7dC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			dynamic requestAuthenticatedUser = Newtonsoft.Json.JsonConvert.DeserializeObject("{ \"ActiveTeam\":null, \"TeamUser\":null, \"ProfilePhotoURL\":null, \"DisplayName\":\"Bradley Don Morris\", \"EmailAddress\":\"bradleydonmorris@hotmail.com\", \"DefaultTeam\":null, \"Teams\":null, \"Profile\":null, \"KeyGUID\":\"00000000-0000-0000-0000-000000000000\" } ");
			using SqlCommand sqlCommand = new();
			String t = sqlCommand.ExecuteScalarString(
				$"[Users].[Authenticate]",
				connectionString,
				new SqlParameter
				{
					ParameterName = "AuthenticatedUserJSON",
					SqlDbType = SqlDbType.NVarChar,
					Size = (-1),
					Value = Newtonsoft.Json.JsonConvert.SerializeObject(requestAuthenticatedUser)
				}
			);

		}
	}
}
