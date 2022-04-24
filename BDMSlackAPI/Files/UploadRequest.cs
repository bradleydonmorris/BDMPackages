using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BDMSlackAPI.Files
{
	[JsonConverter(typeof(UploadRequestConverter))]
	public class UploadRequest : RequestBase
	{
		public static UploadRequest BuildFromFile(String filePath)
		{
			UploadRequest returnValue = new();
			BDMContentTypes.ContentType contentTypeMap = new();
			returnValue.FileType = Path.GetExtension(filePath)[1..];
			returnValue.File = new FileParameter
			{
				FileName = Path.GetFileName(filePath),
				ContentType = contentTypeMap.GetType(filePath),
				Contents = System.IO.File.ReadAllBytes(filePath)
			};
			return returnValue;
		}

		public static UploadRequest BuildFromFile(String filePath, String title, String initalComment, List<String> channels)
		{
			UploadRequest returnValue = UploadRequest.BuildFromFile(filePath);
			returnValue.Title = title;
			returnValue.InitialComment = initalComment;
			returnValue.Channels = channels;
			return returnValue;
		}

		public static UploadRequest BuildFromFile(String filePath, String title, String initalComment, String channel)
		{
			UploadRequest returnValue = UploadRequest.BuildFromFile(filePath);
			returnValue.Title = title;
			returnValue.InitialComment = initalComment;
			returnValue.Channels = new List<String>(new String[] { channel });
			return returnValue;
		}

		public static UploadRequest BuildFromFile(String filePath, String title, String initalComment)
		{
			UploadRequest returnValue = UploadRequest.BuildFromFile(filePath);
			returnValue.Title = title;
			returnValue.InitialComment = initalComment;
			returnValue.Channels = new List<String>();
			return returnValue;
		}

		public UploadRequest()
		{
			this.FileType = "auto";
			this.Channels = new List<String>();
		}

		[JsonProperty("file")]
		public FileParameter File { get; set; }

		/// <summary>
		/// See https://api.slack.com/types/file#file_types for more information
		/// </summary>
		[JsonProperty("filetype")]
		public String FileType { get; set; }

		/// <summary>
		/// Is always equal to this.File.FileName
		/// </summary>
		[JsonProperty("filename")]
		public String FileName
		{
			get
			{
				if (this.File != null)
					return this.File.FileName;
				else
					return null;
			}
			set
			{
				if (this.File != null)
					this.File.FileName = value;
				else
					this.File = new FileParameter()
					{
						FileName = value
					};
			}
		}

		private String _Title;
		[JsonProperty("title")]
		public String Title {
			get
			{
				if (this._Title == null)
				{
					this._Title = this.FileName;
				}
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}

		[JsonProperty("initial_comment")]
		public String InitialComment { get; set; }

		[JsonProperty("channels")]
		public List<String> Channels { get; set; }

		[JsonProperty("thread_ts")]
		public String ThreadTS { get; set; }

		public override Byte[] FormBodyEncode(String formDataBoundary)
		{
			Dictionary<String, Object> attributes = new()
			{
				{ "title", this.Title },
				{ "initial_comment", this.InitialComment },
				{ "channels", String.Join(",", this.Channels) },
				{ "thread_ts", this.ThreadTS },
				{ "pretty", 1 },
				{ "filetype", this.FileType },
				{ "filename", this.FileName },
				{ "file", this.File }
			};
			return base.FormBodyEncodeAttributes(attributes, formDataBoundary);
		}
	}
}
