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
			//Guid asdf = default;
			Provider provider1 = new()
			{
				KeyGUID = Guid.NewGuid(),
				Name = "Me"
			};
			Provider provider2 = new()
			{
				KeyGUID = provider1.KeyGUID,
				Name = "Me"
			};

			Person person1 = new()
			{
				KeyGUID = Guid.NewGuid(),
				FullName = "Bradley Don Morris",
				FirstName = "Bradley",
				LastName = "Morris"
			};
			Person person2 = new()
			{
				KeyGUID = Guid.NewGuid(),
				FullName = "Bradley Don Morris",
				FirstName = "Bradley",
				LastName = "Morris"
			};
			var asdfsadf = provider1.AreKeysEqual(provider2);
			var zsdf = person1.AreKeysEqual(person2);
			var zsdfas = person1.AreKeysEqual(person2, "Name");

			SARTeam.Security.DeviceToken dt1 = new("bradleydonmorris@sar.team", "HP-User", "ThisComputer", "It (1.0.0.0)");
			String temp = dt1.Seal().Serialize(true, true);
			//String teer = "{\"State\": \"Sealed\",\"StateComment\": \"\",\"Hash\": \"zPuiDj8V4vvtQUp4mGI2SdqkbEUW4kDY/1b+x/nxj7KB7a0cBQhbEct6/2RSUjtrx/VKdDeN5kycfV79KbrimOJ0T1arIe+ttfcR1HXrzyQrN8OuqTbcVm6OkfwEgzIpb+6g54p5kKc0PK5Xuu2iVs1H+cHz/14oi15Xjc+Bcw3sKIje3zDO0OhQWSwBcMfQ54utEq2ailbn2u+gj8sJXqsWZ8xltxUAtTiHCb53ZwEmDap4mYBQ6dZk2kVv8YztHeviZVEeJ+cljYrB+ImfAs/0TANSjOtUknGOdJ7dS+3OtinBI7orczQchRsbdOKWiesZZMToULKPfHVo300YLg==\",\"Content\": \"ksPyz/8GSByz7XSA3Tv9BccojVCie3TBrXZTtMWUsvDhrmn/k8rZJjRb0MK2ANbFZVxZJYlCdMhq4vP292gRElDPCsG8Gq/SEwi7twa2E8yo96W29M6dW+YSM3veQQdR3oGlTsk3HrU9e3rh7adfCxxasL9tFB01OEQiOO4ZKUtGWt2BjgkDY/tgXG78WHCvwNXkh/1LFHjS8sljnfirMEy8aIGfLuCaDXRoN9m3HVCF7I7/ZHgIJH3ODGq+U/FIVaFhruxY2DgbAeSAje8us9rNK2G16AMdlDaTh9Tpcpzuh6tp2hJhRiCYAxdZQsLL\",\"Passphrase\": \"YwwmMw1lZV3UOI6TPruuu0frPo2j0sQDH87B9lOUcC2lnTzsS/IFvecC+d9sx4ocGRk/bofjLstZVtLagxikC0xMXAJ6GZKbJH+Fw4zIrzIUfj74itPmYSfvjcIfbf8dSvl4fYFvu5amUptq17Hz7WgJSpLbIv8A8qichyu22NvJGAu8q81NKLGIkbJTNHZ5dzci2z9It9/INNdOqBT35AuLItCGuVIwxgb6lJIhX3FefnDqv1aJgv0yB7QLnPLDnNMqiF6SB8AKmAF5QxMeVocmXFnmXz1ECT7Zt5l559+lo+BUO8B6oYHAN8Pz8niJht+K7msAMRW8Bnl6B+UHRg==\"}";
			SARTeam.Security.DeviceToken? dt = SARTeam.Security.DeviceToken.Open(BDMCiphers.Envelope.Deserialize(temp, true));

			String[] replacementSet = LetterSubsituteSets.CreateNewSet("ABCDEF1234567890");
 			String replacements = LetterSubsituteSets.JoinSet(replacementSet);
			LetterSubsituteSets.ParseSet(replacements);

			String entity = LetterSubsituteSets.GetRandom2Bytes(replacementSet);
			String system = LetterSubsituteSets.GetRandom2Bytes(replacementSet);

			LetterSubstituteIdentifier l = new(replacementSet, entity, system);
			Console.WriteLine($"replacements     {replacements}");
			Console.WriteLine($"Contacts         {LetterSubsituteSets.GetRandom2Bytes(replacementSet)}");
			Console.WriteLine($"Events           {LetterSubsituteSets.GetRandom2Bytes(replacementSet)}");
			Console.WriteLine($"Resources        {LetterSubsituteSets.GetRandom2Bytes(replacementSet)}");
			Console.WriteLine($"Other            {LetterSubsituteSets.GetRandom2Bytes(replacementSet)}");
			Console.WriteLine($"Bradley Laptop   {LetterSubsituteSets.GetRandom2Bytes(replacementSet)}");


			String TheString = "ybseybseybybybse";
			String seg1 = TheString[..4];
			String seg2 = TheString[4..8];
			String seg3 = TheString[^8..];

			Console.WriteLine($"seg1        {seg1}");
			Console.WriteLine($"seg1[..2]   {seg1[..2]}");
			Console.WriteLine($"seg1[^2..]  {seg1[^2..]}");
			Console.WriteLine("");
			Console.WriteLine($"seg2        {seg2}");
			Console.WriteLine($"seg2[..2]   {seg2[..2]}");
			Console.WriteLine($"seg2[^2..]  {seg2[^2..]}");

			Console.WriteLine("");
			Console.WriteLine($"seg3        {seg3}");
			Console.WriteLine($"seg3[..2]   {seg3[..2]}");
			Console.WriteLine($"seg3[2..4]   {seg3[2..4]}");
			Console.WriteLine($"seg3[4..6]   {seg3[4..6]}");
			Console.WriteLine($"seg3[^2..]  {seg3[^2..]}");

			//BDMEntityNumbering.SequentialIdentifier s = new("ABC0001000200000003XYZ");
			//String four = s.GetNext();
			//String five = s.GetNext();


			BDMEntityNumbering.LetterSubstituteIdentifier s2 = new(String.Empty, String.Empty, "ybseybseybybybse");
			String eight = s2.Increment();

			//BDMCommandLine.ICommand 

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
