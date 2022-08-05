using System;

namespace BDMSqliteBuilder
{
	public class View
	{
		public Int32 CreationOrder { get; set; }
		public String Name { get; set; }
		public String Query { get; set; }

		public View()
		{
			this.Name = String.Empty;
			this.Query = String.Empty;
		}

		public override String ToString()
			=> this.Query.EndsWith(";")
				? $"CREATE VIEW \"{this.Name}\"\r\nAS\r\n{this.Query}\r\n"
				: $"CREATE VIEW \"{this.Name}\"\r\nAS\r\n{this.Query};\r\n";
	}
}
