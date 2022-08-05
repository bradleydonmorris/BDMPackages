using System;
using System.Collections.Generic;
using System.Linq;

namespace BDMSqliteBuilder
{
	public class Table
	{
		public Int32 CreationOrder { get; set; }
		public String Name { get; set; }
		public List<Column> Columns { get; set; }
		public List<ForeignKey> ForeignKeys { get; set; }

		public Int32 NextOrdinalPosition
			=> (this.Columns.Count > 1)
				? (this.Columns.Max(c => c.OrdinalPosition) + 1)
				: 1;
		
		public Table()
		{
			this.Name = String.Empty;
			this.Columns = new();
			this.ForeignKeys = new();
		}

		public void AppendColumn(Column column)
		{
			column.OrdinalPosition = this.NextOrdinalPosition;
			this.Columns.Add(column);
		}

		public void AppendColumn(
			String name, DataType dataType,
			Boolean isNullable, Boolean isPrimaryKey, Boolean isAutoIncrement, Boolean isUnique,
			String defaultValue)
			=> this.AppendColumn(new()
			{
				Name = name,
				DataType = dataType,
				IsNullable = isNullable,
				IsPrimaryKey = isPrimaryKey,
				IsAutoIncrement = isAutoIncrement,
				IsUnique = isUnique,
				DefaultValue = defaultValue
			});
		public void AppendColumn(String name, DataType dataType)
			=> this.AppendColumn(new()
			{
				Name = name,
				DataType = dataType,
				IsNullable = true,
				IsPrimaryKey = false,
				IsAutoIncrement = false,
				IsUnique = false,
				DefaultValue = null
			});
		public void AppendColumn(String name, DataType dataType, Boolean isNullable)
			=> this.AppendColumn(new()
			{
				Name = name,
				DataType = dataType,
				IsNullable = isNullable,
				IsPrimaryKey = false,
				IsAutoIncrement = false,
				IsUnique = false,
				DefaultValue = null
			});
		public void AppendColumn(String name, DataType dataType, Boolean isNullable, Boolean isUnique)
			=> this.AppendColumn(new()
			{
				Name = name,
				DataType = dataType,
				IsNullable = isNullable,
				IsPrimaryKey = false,
				IsAutoIncrement = false,
				IsUnique = isUnique,
				DefaultValue = null
			});
		public void AppendColumn(String name, DataType dataType, String referencedTable, String referencedColumn)
		{
			this.AppendColumn(new()
			{
				Name = name,
				DataType = dataType,
				IsNullable = false,
				IsPrimaryKey = false,
				IsAutoIncrement = false,
				IsUnique = false,
				DefaultValue = null
			});
			this.AppendForeignKey(name, referencedTable, referencedColumn);
		}

		public void AppendKey()
		{
			this.AppendColumn(new()
			{
				Name = "Key",
				DataType = DataType.Text,
				IsNullable = false,
				IsPrimaryKey = false,
				IsAutoIncrement = false,
				IsUnique = true,
				DefaultValue = null
			});
		}

		public void AppendKeyGUID()
		{
			this.AppendColumn(new()
			{
				Name = "KeyGUID",
				DataType = DataType.Text,
				IsNullable = false,
				IsPrimaryKey = false,
				IsAutoIncrement = false,
				IsUnique = true,
				DefaultValue = null
			});
		}

		public void AppendForeignKey(ForeignKey foreignKey)
			=> this.ForeignKeys.Add(foreignKey);

		public void AppendForeignKey(String column, String referencedTable, String referencedColumn)
			=> this.AppendForeignKey(new()
			{
				Column = column,
				ReferencedTable = referencedTable,
				ReferencedColumn = referencedColumn
			});

		/// <summary>
		/// Returns code used in the creation of a table
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			String returnValue = $"CREATE TABLE \"{this.Name}\"\r\n(";
			Int32 loopCount = 0;
			List<Column> primaryKeyColumns = new();
			if (!this.Columns.Any(c => c.IsAutoIncrement))
				primaryKeyColumns = this.Columns.Where(c => c.IsPrimaryKey).ToList();
			var foreignKeys = new List<dynamic>();
			if (this.ForeignKeys.Count > 0)
			{
				foreach (ForeignKey foreignKey in this.ForeignKeys)
				{
					if (this.Columns.Any(c => c.Name == foreignKey.Column))
						foreignKeys.Add(
							new
							{
								this.Columns.Find(c => c.Name == foreignKey.Column)?.OrdinalPosition,
								foreignKey.Column,
								foreignKey.ReferencedTable,
								foreignKey.ReferencedColumn,
							}
						);
				}
			}
			Boolean hasForeignKeys = (foreignKeys.Count > 0);
			Boolean hasPrimaryKey = (primaryKeyColumns.Count > 0);
			if (this.Columns.Count > 1)
			{
				loopCount = 0;
				foreach (Column column in this.Columns.OrderBy(c => c.OrdinalPosition))
				{
					loopCount ++;
					if (loopCount < this.Columns.Count)
						returnValue += $"\r\n{column},";
					else
						if (hasForeignKeys || hasPrimaryKey)
							returnValue += $"\r\n{column},";
						else
							returnValue += $"\r\n{column}";
				}
			}
			if (
				primaryKeyColumns is not null
				&& primaryKeyColumns.Count > 0)
			{
				returnValue += "\r\n\tPRIMARY KEY(";
				loopCount = 0;
				foreach (Column column in primaryKeyColumns.OrderBy(c => c.OrdinalPosition))
				{
					loopCount ++;
					if (loopCount < primaryKeyColumns.Count)
						returnValue += $"\"{column.Name}\",";
					else
						returnValue += $"{column.Name}\"";
				}
				if (hasForeignKeys)
					returnValue += "),";
				else
					returnValue += ")";
			}
			if (foreignKeys.Count > 0)
			{
				loopCount = 0;
				foreach (dynamic foreignKey in foreignKeys.OrderBy(f => f.OrdinalPosition))
				{
					if (foreignKey is not null)
					{
						loopCount ++;
						if (loopCount < foreignKeys.Count)
							returnValue += $"\r\n\tFOREIGN KEY(\"{foreignKey.Column}\") REFERENCES \"{foreignKey.ReferencedTable}\"(\"{foreignKey.ReferencedColumn}\"),";
						else
							returnValue += $"\r\n\tFOREIGN KEY(\"{foreignKey.Column}\") REFERENCES \"{foreignKey.ReferencedTable}\"(\"{foreignKey.ReferencedColumn}\")";
					}
				}
			}
			returnValue += "\r\n);";
			return returnValue;
		}

		public static Table CreateWithIdentity(String name)
			=> new()
			{
				Name = name,
				Columns = new List<Column>()
				{
					new()
					{
 						Name = $"{name}Id",
						DataType = DataType.Integer,
						IsNullable = false,
						IsPrimaryKey = true,
						IsAutoIncrement = true,
						IsUnique = true
					}
				}
			};

		public static Table CreateLookup(String name)
			=> new()
			{
				Name = name,
				Columns = new List<Column>()
				{
					new()
					{
 						Name = $"{name}Id",
						DataType = DataType.Integer,
						IsNullable = false,
						IsPrimaryKey = true,
						IsAutoIncrement = true,
						IsUnique = true
					},
					new()
					{
 						Name = $"Name",
						DataType = DataType.Text,
						IsNullable = false,
						IsPrimaryKey = false,
						IsAutoIncrement = false,
						IsUnique = true
					}
				}
			};
	}
}
