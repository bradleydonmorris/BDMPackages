using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace BDMDelimitedFile
{
	public class DelimitedFile
	{
		public DelimitedFile() { }

		public DelimitedFile(String fileName)
		{
			this.FilePath = fileName;
		}

		public DelimitedFile(String fileName, String columnDelimiter = ",")
		{
			this.FilePath = fileName;
			this.ColumnDelimiter = columnDelimiter;
		}

		public String FilePath { get; set; } = String.Empty;
		public String ColumnDelimiter { get; set; } = ",";
		public Boolean HasHeaderRow { get; set; } = true;

		public DataTable ToDataTable()
		{
			DataTable returnValue = new();
			if (!File.Exists(this.FilePath))
				throw new FileNotFoundException();
			using TextFieldParser textFieldParser = new(this.FilePath);
			textFieldParser.TextFieldType = FieldType.Delimited;
			textFieldParser.Delimiters = [this.ColumnDelimiter];
			Boolean isFirstRow = true;
			String[]? headerRow;
			while (!textFieldParser.EndOfData)
			{
				if (this.HasHeaderRow && isFirstRow)
				{
					isFirstRow = false;
					headerRow = textFieldParser.ReadFields();
					if (headerRow is not null)
						foreach (String field in headerRow)
							returnValue.Columns.Add(field, typeof(String));
				}
				else if (!this.HasHeaderRow && isFirstRow)
				{
					isFirstRow = false;
					String[]? dataRow = textFieldParser.ReadFields();
					if (dataRow is not null)
					{
						for (Int32 columnIndex = 0; columnIndex < dataRow.Length; columnIndex ++)
							returnValue.Columns.Add($"Column_{columnIndex}", typeof(String));
						returnValue.Rows.Add(dataRow);
					}
				}
				else
				{
					String[]? dataRow = textFieldParser.ReadFields();
					if (dataRow is not null)
						returnValue.Rows.Add(dataRow);
				}
			}
			return returnValue;
		}

		public void FromDataTable(DataTable dataTable)
		{
			StreamWriter streamWriter = new(this.FilePath, false);
			if (this.HasHeaderRow)
			{
				String line = String.Empty;
				foreach (DataColumn dataColumn in dataTable.Columns)
					line +=
						dataColumn.ColumnName.Contains(this.ColumnDelimiter)
							? $"\"{dataColumn.ColumnName}\"{this.ColumnDelimiter}"
							: $"{dataColumn.ColumnName}{this.ColumnDelimiter}"
					;
				if (line.EndsWith(this.ColumnDelimiter))
					line = line[..^this.ColumnDelimiter.Length];
				streamWriter.WriteLine($"{line}");
			}
			foreach (DataRow dataRow in dataTable.Rows)
			{
				String line = String.Empty;
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					String value = dataRow[dataColumn.ColumnName]?.ToString() ?? String.Empty;
					line +=
						value.Contains(this.ColumnDelimiter)
							? $"\"{value}\"{this.ColumnDelimiter}"
							: $"{value}{this.ColumnDelimiter}"
					;
				}
				if (line.EndsWith(this.ColumnDelimiter))
					line = line[..^this.ColumnDelimiter.Length];
				streamWriter.WriteLine($"{line}");
			}
			streamWriter.Flush();
			streamWriter.Close();
		}
	}
}
