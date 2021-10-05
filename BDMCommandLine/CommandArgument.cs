using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public class CommandArgument
	{
		public String Name { get; set; }
		public String Alias { get; set; }
		public String Description { get; set; }
		public Boolean IsRequired { get; set; }
		public String[] Options { get; set; }
		public Boolean IsProvided { get; set; }
		public Boolean IsFlag { get; set; }

		public Boolean IsFlagedTrue => (this.IsFlag && this.IsProvided);

		public Boolean IsVerified { get; set; }
		public String Value { get; set; }

		public CommandArgument()
		{
			this.IsVerified = false;
			this.IsProvided = false;
			this.IsFlag = false;
		}

		public ConsoleText[] GetHelpText()
		{
			List<ConsoleText> returnValue = new();
			String required = this.IsRequired ? "REQUIRED. " : "";
			String usage = $"   --{this.Name.ToLower()}, -{this.Alias.ToLower()}".PadRight(20);
			returnValue.Add(ConsoleText.Default($"{usage}{required}{this.Description}"));
			if (this.Options != null)
				returnValue.Add(ConsoleText.Default($"\n                    Options: {String.Join(", ", this.Options)}"));
			return returnValue.ToArray();
		}
		public String SetValue(String value)
		{
			String returnValue = null;
			if (
				String.IsNullOrWhiteSpace(value)
				&& this.IsRequired
			)
			{
				this.IsVerified = false;
				returnValue = $"{this.Name}: Value is required.";
			}
			if (this.Options != null && this.Options.Length > 0)
			{
				if (this.Options.Contains(value))
				{
					this.IsProvided = true;
					this.IsVerified = true;
					this.Value = value;
				}
				else
				{
					this.IsProvided = false;
					this.IsVerified = false;
					returnValue = $"{this.Name}: \"{value}\" is not an acceptable option.";
				}
			}
			else if (
				this.IsFlag
				&&
				(
					String.IsNullOrWhiteSpace(value)
					|| value.ToLower() == "true"
				)
			)
			{
				this.IsProvided = true;
				this.IsVerified = true;
				this.Value = value;
			}
			return returnValue;
		}
		public String GetValue()
		{
			return this.Value ?? "";
		}
	}
}
