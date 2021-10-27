using Microsoft.Data.SqlClient;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System;
using System.Data;

namespace BDMSerilogProc
{
	public class BDMSerilogProcSink : ILogEventSink
    {
        private readonly String _ConnectionString;
        private readonly String _ProcSchema;
        private readonly String _ProcName;
        private readonly String _InputParamName;

        public BDMSerilogProcSink() { }

        public BDMSerilogProcSink(
            String connectionString,
            String procSchema = "Logging",
            String procName = "Enqueue",
            String inputParamName = "JSON"
        )
        {
            this._ConnectionString = connectionString;
            this._ProcSchema = procSchema;
            this._ProcName = procName;
            this._InputParamName = inputParamName;
        }


        public void Emit(LogEvent logEvent)
        {
            try
            {
                SqlCommand command = new();
                command.Connection = new(this._ConnectionString);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = $"[{this._ProcSchema}].[{this._ProcName}]";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = this._InputParamName,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = (-1),
                    Value = Newtonsoft.Json.JsonConvert.SerializeObject(new LogableObject(logEvent))
                });
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
                command.ExecuteNonQueryAsync();
                if (command.Connection.State != ConnectionState.Closed)
                    command.Connection.Close();
                command.Connection.Dispose();
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Unable to write log event to the database due to following error: {1}", ex.Message);
                throw;
            }
        }
    }
}
