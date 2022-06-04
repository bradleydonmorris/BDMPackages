 using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace BDMJsonProcs
{
	// TODO: Build better XML comments
	public static class SqlCommandExtensions
	{
		#region No Response
		///<summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			sqlCommand.ExecuteNoResponse("[Widgets].[Delete]", this._ConnectionString,
		///				new SqlParameter { ParameterName = "WidgetGUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = guid }
		///			);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="parameters"></param>
		public static void ExecuteNoResponse(
			this SqlCommand command,
			String commandText,
			String connectionString,
			params SqlParameter[] parameters)
		{
			command.Connection = new SqlConnection(connectionString);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = commandText;
			foreach (SqlParameter parameter in parameters)
				command.Parameters.Add(parameter);
			if (command.Connection.State != ConnectionState.Open)
				command.Connection.Open();
			command.ExecuteNonQuery();
			if (command.Connection.State != ConnectionState.Closed)
				command.Connection.Close();
			command.Connection.Dispose();
		}

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return sqlCommand.ExecuteNoResponse("[Widgets].[FindWidgets]", this._ConnectionString,
		///											new WidgetSearchOptions()
		///											{
		///												NameLike = "Cool%",
		///												DescriptionLike = "%red%",
		///												Type = "Bicycle"
		///											},
		///											"MyJSONStringParameters"
		///										);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="inputObject"></param>
		/// <param name="parameterName"></param>
		public static void ExecuteNoResponse(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON")
		{
			command.Connection = new SqlConnection(connectionString);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = commandText;
			command.Parameters.Add(new SqlParameter
			{
				ParameterName = parameterName,
				SqlDbType = SqlDbType.NVarChar,
				Size = (-1),
				Value = Newtonsoft.Json.JsonConvert.SerializeObject(inputObject)
			});
			if (command.Connection.State != ConnectionState.Open)
				command.Connection.Open();
			if (command.Connection.State != ConnectionState.Open)
				command.Connection.Open();
			command.ExecuteNonQuery();
			if (command.Connection.State != ConnectionState.Closed)
				command.Connection.Close();
			command.Connection.Dispose();
		}
		#endregion No Response


		#region Return desialized Object
		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return sqlCommand.ExecuteScalarJsonProc<IEnumerable<Widget>>("[Widgets].[GetAll]", this._ConnectionString);
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <returns>TResult deserialized from JSON</returns>
		public static TResult? ExecuteScalarJsonProc<TResult>(
			this SqlCommand command,
			String commandText,
			String connectionString)
			where TResult : class, new()
		{
			TResult? returnValue = null;
			String? scalarResult = command.ExecuteScalarJsonProc(
				commandText,
				connectionString
			);
			if (!String.IsNullOrWhiteSpace(scalarResult))
				returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(scalarResult);
			return returnValue;
		}

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return (sqlCommand.ExecuteScalarJsonProc<Widget>("[Widgets].[Get]", this._ConnectionString,
		///				new SqlParameter { ParameterName = "WidgetGUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = guid }
		///			);
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="parameters"></param>
		/// <returns>TResult deserialized from JSON</returns>
		public static TResult? ExecuteScalarJsonProc<TResult>(
			this SqlCommand command,
			String commandText,
			String connectionString,
			params SqlParameter[] parameters)
			where TResult : class, new()
		{
			TResult? returnValue = null;
			String? scalarResult = command.ExecuteScalarJsonProc(
				commandText,
				connectionString,
				parameters
			);
			if (!String.IsNullOrWhiteSpace(scalarResult))
				returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(scalarResult);
			return returnValue;
		}

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return sqlCommand.ExecuteScalarJsonProc<IEnumerable<Widget>>("[Widgets].[FindWidgets]", this._ConnectionString,
		///											new WidgetSearchOptions()
		///											{
		///												NameLike = "Cool%",
		///												DescriptionLike = "%red%",
		///												Type = "Bicycle"
		///											});
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="inputObject"></param>
		/// <param name="parameterName"></param>
		/// <returns>TResult deserialized from JSON</returns>
		public static TResult? ExecuteScalarJsonProc<TResult>(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON")
			where TResult : class, new()
		{
			TResult? returnValue = null;
			String? scalarResult = command.ExecuteScalarJsonProc(
				commandText,
				connectionString,
				inputObject,
				parameterName
			);
			if (!String.IsNullOrWhiteSpace(scalarResult))
				returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(scalarResult);
			return returnValue;
		}
		#endregion Return desialized Object

		#region Return String
		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return sqlCommand.ExecuteScalarJsonProc<IEnumerable<Widget>>("[Widgets].[GetAll]", this._ConnectionString);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <returns>JSON String</returns>
		public static String? ExecuteScalarJsonProc(
			this SqlCommand command,
			String commandText,
			String connectionString)
			=> command.ExecuteScalarString(
				commandText,
				connectionString
			);

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return (sqlCommand.ExecuteScalarJsonProc("[Widgets].[Get]", this._ConnectionString,
		///				new SqlParameter { ParameterName = "WidgetGUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = guid }
		///			);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="parameters"></param>
		/// <returns>JSON String</returns>
		public static String? ExecuteScalarJsonProc(
			this SqlCommand command,
			String commandText,
			String connectionString,
			params SqlParameter[] parameters)
			=> command.ExecuteScalarString(
				commandText,
				connectionString,
				parameters
			);

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return sqlCommand.ExecuteScalarJsonProc("[Widgets].[FindWidgets]", this._ConnectionString,
		///											new WidgetSearchOptions()
		///											{
		///												NameLike = "Cool%",
		///												DescriptionLike = "%red%",
		///												Type = "Bicycle"
		///											});
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="inputObject"></param>
		/// <param name="parameterName"></param>
		/// <returns>JSON String</returns>
		public static String? ExecuteScalarJsonProc(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON")
			=> command.ExecuteScalarString(
				commandText,
				connectionString,
				new SqlParameter
				{
					ParameterName = parameterName,
					SqlDbType = SqlDbType.NVarChar,
					Size = (-1),
					Value = Newtonsoft.Json.JsonConvert.SerializeObject(inputObject)
				}
			);
		#endregion Return String 

		#region SqlCommand.ExecuteScalar Replacement
		/// <summary>
		/// Used to overcome limitations of SqlCommand.ExecuteScalar
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <returns>String</returns>
		public static String? ExecuteScalarString(
			this SqlCommand command,
			String commandText,
			String connectionString)
		{
			String? returnValue = null;
			command.Connection = new SqlConnection(connectionString);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = commandText;
			if (command.Connection.State != ConnectionState.Open)
				command.Connection.Open();
			SqlDataReader sqlDataReader = command.ExecuteReader();
			while (sqlDataReader.Read())
				returnValue = sqlDataReader.GetString(0);
			if (command.Connection.State != ConnectionState.Closed)
				command.Connection.Close();
			command.Connection.Dispose();
			return returnValue;
		}


		/// <summary>
		/// Used to overcome limitations of SqlCommand.ExecuteScalar
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="parameters"></param>
		/// <returns>String</returns>
		public static String? ExecuteScalarString(
			this SqlCommand command,
			String commandText,
			String connectionString,
			params SqlParameter[] parameters)
		{
			String? returnValue = null;
			command.Connection = new SqlConnection(connectionString);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = commandText;
			foreach (SqlParameter parameter in parameters)
				command.Parameters.Add(parameter);
			if (command.Connection.State != ConnectionState.Open)
				command.Connection.Open();
			SqlDataReader sqlDataReader = command.ExecuteReader();
			while (sqlDataReader.Read())
				returnValue = sqlDataReader.GetString(0);
			if (command.Connection.State != ConnectionState.Closed)
				command.Connection.Close();
			command.Connection.Dispose();
			return returnValue;
		}
		#endregion SqlCommand.ExecuteScalar Replacement
	}
}
