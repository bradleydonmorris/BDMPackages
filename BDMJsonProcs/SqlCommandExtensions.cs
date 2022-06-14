 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace BDMJsonProcs
{
	// TODO: Build better XML comments
	public static class SqlCommandExtensions
	{
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
		/// <param name="parameters"></param>
		/// <returns>TResult deserialized from JSON</returns>
		public static TResult? ExecuteScalarJsonProc<TResult>(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON",
			params SqlParameter[] parameters)
			where TResult : class, new()
		{
			TResult? returnValue = null;
			String? scalarResult = command.ExecuteScalarJsonProc(
				commandText,
				connectionString,
				inputObject,
				parameterName,
				parameters
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

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			return sqlCommand.ExecuteScalarJsonProc("[Widgets].[FindWidgets]", this._ConnectionString,
		///									new WidgetSearchOptions()
		///									{
		///										NameLike = "Cool%",
		///										DescriptionLike = "%red%",
		///										Type = "Bicycle"
		///									},
		///									new SqlParameter { ParameterName = "WidgetGUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = guid }
		///								);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="inputObject"></param>
		/// <param name="parameterName"></param>
		/// <param name="parameters"></param>
		/// <returns>JSON String</returns>
		public static String? ExecuteScalarJsonProc(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON",
			params SqlParameter[] parameters)
		{
			List<SqlParameter> parametersList = new(parameters);
			parametersList.Add(
				new SqlParameter
				{
					ParameterName = parameterName,
					SqlDbType = SqlDbType.NVarChar,
					Size = (-1),
					Value = Newtonsoft.Json.JsonConvert.SerializeObject(inputObject)
				}
			);
			return command.ExecuteScalarString(
				commandText,
				connectionString,
				parametersList.ToArray()
			);
		}
		#endregion Return String 

		#region Return void
		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			sqlCommand.ExecuteJsonProcNoResults<IEnumerable<Widget>>("[Widgets].[Post]", this._ConnectionString);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		public static void ExecuteJsonProcNoResults(
			this SqlCommand command,
			String commandText,
			String connectionString)
			=> command.ExecuteNoResults(
				commandText,
				connectionString
			);

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			(sqlCommand.ExecuteJsonProcNoResults("[Widgets].[Post]", this._ConnectionString,
		///				new SqlParameter { ParameterName = "WidgetGUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = guid }
		///			);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="parameters"></param>
		public static void ExecuteJsonProcNoResults(
			this SqlCommand command,
			String commandText,
			String connectionString,
			params SqlParameter[] parameters)
			=> command.ExecuteNoResults(
				commandText,
				connectionString,
				parameters
			);

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			sqlCommand.ExecuteJsonProcNoResults("[Widgets].[Post]", this._ConnectionString,
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
		public static void ExecuteJsonProcNoResults(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON")
			=> command.ExecuteNoResults(
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

		/// <summary>
		/// Example Usage:
		///		using (SqlCommand sqlCommand = new())
		///			sqlCommand.ExecuteJsonProcNoResults("[Widgets].[Post]", this._ConnectionString,
		///									new WidgetSearchOptions()
		///									{
		///										NameLike = "Cool%",
		///										DescriptionLike = "%red%",
		///										Type = "Bicycle"
		///									},
		///									new SqlParameter { ParameterName = "WidgetGUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = guid }
		///								);
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="inputObject"></param>
		/// <param name="parameterName"></param>
		/// <param name="parameters"></param>
		public static void ExecuteJsonProcNoResults(
			this SqlCommand command,
			String commandText,
			String connectionString,
			Object inputObject,
			String parameterName = "JSON",
			params SqlParameter[] parameters)
		{
			List<SqlParameter> parametersList = new(parameters);
			parametersList.Add(
				new SqlParameter
				{
					ParameterName = parameterName,
					SqlDbType = SqlDbType.NVarChar,
					Size = (-1),
					Value = Newtonsoft.Json.JsonConvert.SerializeObject(inputObject)
				}
			);
			command.ExecuteNoResults(
				commandText,
				connectionString,
				parametersList.ToArray()
			);
		}
		#endregion Return void

		#region SqlCommand.Execute{xxx} Replacement
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

		/// <summary>
		/// Used to overcome limitations of SqlCommand.ExecuteScalar
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		public static void ExecuteNoResults(
			this SqlCommand command,
			String commandText,
			String connectionString)
		{
			command.Connection = new SqlConnection(connectionString);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = commandText;
			if (command.Connection.State != ConnectionState.Open)
				command.Connection.Open();
			command.ExecuteNonQuery();
			if (command.Connection.State != ConnectionState.Closed)
				command.Connection.Close();
			command.Connection.Dispose();
		}

		/// <summary>
		/// Wrapper for ExecuteNonQuery
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandText"></param>
		/// <param name="connectionString"></param>
		/// <param name="parameters"></param>
		public static void ExecuteNoResults(
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
		#endregion SqlCommand.Execute{xxx} Replacement
	}
}
