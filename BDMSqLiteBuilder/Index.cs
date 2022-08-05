using System;
using System.Collections.Generic;

namespace BDMSqliteBuilder
{
	public class Index
	{
		public Int32 CreationOrder { get; set; }
		public String Name { get; set; }
		public String Table { get; set; }
		public List<String> Columns { get; set; }
		public Boolean IsUnique { get; set; }

		public Index()
		{
			this.Name = String.Empty;
			this.Table = String.Empty;
			this.Columns = new();
		}

		private String GetQuotedColumns()
		{
			List<String> columns = new();
			foreach (String column in this.Columns)
				columns.Add($"\"{column}\"");
			return String.Join(", ", columns);
		}

		public override String ToString()
			=> this.IsUnique
				? $"CREATE UNIQUE INDEX \"{this.Name}\"\r\n\tON {this.Table}\r\n\t({this.GetQuotedColumns()});"
				: $"CREATE INDEX \"{this.Name}\"\r\n\tON {this.Table}\r\n\t({this.GetQuotedColumns()});";
	}
}
