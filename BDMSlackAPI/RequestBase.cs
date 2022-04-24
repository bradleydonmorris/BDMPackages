using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace BDMSlackAPI
{
	public class RequestBase
	{
		[JsonProperty("token")]
		public String Token { get; set; }

		[JsonProperty("pretty")]
		public Int32 Pretty { get; set; }

		public RequestBase()
		{
			this.Pretty = 1;
		}

		public virtual String FormURLEncode()
		{
			return null;
		}

		public virtual Byte[] FormBodyEncode(String formDataBoundary)
		{
			return null;
		}

		public String FormURLEncodeAttributes(Dictionary<String, Object> attributes)
		{
            List<String> queries = new()
            {
                String.Format("token={0}", this.Token)
            };
            foreach (var attribute in attributes)
			{
				queries.Add(String.Format("{0}={1}", attribute.Key, HttpUtility.UrlEncode(attribute.Value.ToString())));
			}
			return String.Join("&", queries);
		}

		public Byte[] FormBodyEncodeAttributes(Dictionary<String, Object> attributes, String formDataBoundary)
		{
            Stream formDataStream = new System.IO.MemoryStream();
			String tokenField = String.Format
			(
				"--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
				formDataBoundary,
				"token",
				this.Token
			);
			formDataStream.Write(Encoding.UTF8.GetBytes(tokenField), 0, Encoding.UTF8.GetByteCount(tokenField));
			foreach (var attribute in attributes)
			{
				formDataStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));
				if (attribute.Value is FileParameter fileToUpload)
				{
					String header = String.Format
					(
						"--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
						formDataBoundary,
						attribute.Key,
						fileToUpload.FileName ?? attribute.Key,
						fileToUpload.ContentType ?? "application/octet-stream"
					);
					formDataStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
					formDataStream.Write(fileToUpload.Contents, 0, fileToUpload.Contents.Length);
				}
				else
				{
					String value = "";
					if (attribute.Value != null)
						value = attribute.Value.ToString();
					String field = String.Format
					(
						"--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
						formDataBoundary,
						attribute.Key,
						value
					);
					formDataStream.Write(Encoding.UTF8.GetBytes(field), 0, Encoding.UTF8.GetByteCount(field));
				}
			}
			String footer = "\r\n--" + formDataBoundary + "--\r\n";
			formDataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));

			formDataStream.Position = 0;
            Byte[] returnValue = new byte[formDataStream.Length];
            formDataStream.Read(returnValue, 0, returnValue.Length);
			formDataStream.Close();
			return returnValue;

		}
	}
}
