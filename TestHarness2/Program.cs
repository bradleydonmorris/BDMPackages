// See https://aka.ms/new-console-template for more information
using BDMCommandLine;
using SQLServerPostgreSQLInterface.Commands;

ConsoleText.DefaultForegroundColor = Console.ForegroundColor;
ConsoleText.DefaultBackgroundColor = Console.BackgroundColor;


Console.WriteLine("Hello, World!");
CommandLine commandLine = new();
commandLine.AddCommand(new SQLServer2PostgreSQL());

if (args == null)
{
	Console.WriteLine("Its null");
}
if (commandLine.ParseArguments(args))
{
	commandLine.Execute();
	Console.ResetColor();
}



/*
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


*/