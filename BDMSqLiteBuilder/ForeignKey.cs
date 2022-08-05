using System;

namespace BDMSqliteBuilder
{
	public class ForeignKey
	{
		public String Column { get; set; }
		public String ReferencedTable { get; set; }
		public String ReferencedColumn { get; set; }

		public ForeignKey()
		{
			this.Column = String.Empty;
			this.ReferencedTable = String.Empty;
			this.ReferencedColumn = String.Empty;
		}
	}
}
