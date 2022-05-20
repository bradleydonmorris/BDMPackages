using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BDMSlackAPI
{
	public class FileParameter
	{
		public Byte[] Contents { get; set; }
		public String FileName { get; set; }
		public String ContentType { get; set; }

		public ByteArrayContent ByteArrayContent() => new(this.Contents, 0, this.Contents.Length);
	}
}
