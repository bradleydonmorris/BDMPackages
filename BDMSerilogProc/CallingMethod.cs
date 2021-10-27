using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMSerilogProc
{
	public class CallingMethod
	{
		public String Name { get; set; }
		public String ClassName { get; set; }

		public override String ToString()
		{
			return $"{this.ClassName}.{this.Name}";
		}
	}
}
