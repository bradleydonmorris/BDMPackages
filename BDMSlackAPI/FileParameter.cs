using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI
{
	public class FileParameter
	{
		public Byte[] Contents { get; set; }
		public String FileName { get; set; }
		public String ContentType { get; set; }
	}
}
