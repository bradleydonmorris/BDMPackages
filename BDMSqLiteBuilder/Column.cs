using System;

namespace BDMSqliteBuilder
{
	public class Column
	{
		public Int32 OrdinalPosition { get; set; }
		public String Name { get; set; }
		public DataType DataType { get; set; }
		public Boolean IsNullable { get; set; }
		public Boolean IsPrimaryKey { get; set; }

		private Boolean _isAutoIncrement;
		public Boolean IsAutoIncrement
		{
			get => this._isAutoIncrement;
			set
			{
				this._isAutoIncrement = value;
				if (this._isAutoIncrement)
				{
					this.IsPrimaryKey = true;
					this.IsNullable = false;
				}
			}
		}
		public Boolean IsUnique { get; set; }
		public String DefaultValue { get; set; }

		public Column()
			=> this.Name = String.Empty;

		/// <summary>
		/// Returns code used in the creation of a table
		/// </summary>
		/// <returns></returns>
		public override String ToString()
			=> $"\t\"{this.Name}\" {this.DataType.ToString().ToUpper()}"
				+ (!this.IsNullable ? " NOT NULL" : "")
				+ (this.IsAutoIncrement ? " PRIMARY KEY AUTOINCREMENT" : "")
				+ (this.IsUnique ? " UNIQUE" : "")
				+ ((this.DefaultValue is not null) ? $" DEFAULT {this.DefaultValue}" : "");

		public static Column CreateIdentity(String name)
			=> new()
			{
				Name = name,
				DataType = DataType.Integer,
				IsNullable = false,
				IsPrimaryKey = true,
				IsAutoIncrement = true,
				IsUnique = true
			};

	}
}
