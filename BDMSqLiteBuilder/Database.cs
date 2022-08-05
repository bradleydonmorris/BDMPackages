using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDMSqliteBuilder
{
	public class Database
	{
		public List<Table> Tables { get; set; }
		public List<View> Views { get; set; }
		public List<Index> Indexes { get; set; }

		public Int32 NextTableCreationOrder
			=> (this.Tables.Count > 1)
				? (this.Tables.Max(c => c.CreationOrder) + 1)
				: 1;

		public Int32 NextViewCreationOrder
			=> (this.Views.Count > 1)
				? (this.Views.Max(c => c.CreationOrder) + 1)
				: 1;

		public Int32 NextIndexCreationOrder
			=> (this.Indexes.Count > 1)
				? (this.Indexes.Max(c => c.CreationOrder) + 1)
				: 1;

		public Database()
		{
			this.Tables = new();
			this.Views = new();
			this.Indexes = new();
		}

		public void AppendTable(Table table)
		{
			table.CreationOrder = this.NextTableCreationOrder;
			this.Tables.Add(table);
		}

		public void AppendView(View view)
		{
			view.CreationOrder = this.NextViewCreationOrder;
			this.Views.Add(view);
		}

		public void AppendIndex(Index index)
		{
			index.CreationOrder = this.NextViewCreationOrder;
			this.Indexes.Add(index);
		}

		/// <summary>
		/// Returns code used in the creation of a database
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			StringBuilder returnValue = new();
			foreach (Table table in this.Tables.OrderBy(t => t.CreationOrder))
				returnValue.AppendLine(table.ToString() + "\r\n");
			foreach (View view in this.Views.OrderBy(v => v.CreationOrder))
				returnValue.AppendLine(view.ToString() + "\r\n");
			foreach (Index index in this.Indexes.OrderBy(i => i.CreationOrder))
				returnValue.AppendLine(index.ToString() + "\r\n");
			return returnValue.ToString();
		}

	}
}