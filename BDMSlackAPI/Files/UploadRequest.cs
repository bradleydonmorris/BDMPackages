using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

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
				if (this.File is not null)
					return this.File.FileName;
				else
					return null;
			}
			set
			{
				if (this.File is not null)
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
				if (this._Title is null)
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

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("pretty", base.Pretty.ToString());
			yield return new KeyValuePair<String, String>("title", this.Title);
			yield return new KeyValuePair<String, String>("initial_comment", this.InitialComment);
			yield return new KeyValuePair<String, String>("channels", String.Join(",", this.Channels));
			yield return new KeyValuePair<String, String>("thread_ts", this.ThreadTS);
			yield return new KeyValuePair<String, String>("filetype", this.FileType);
			yield return new KeyValuePair<String, String>("filename", this.FileName);
		}

		public override MultipartFormDataContent MultipartFormDataContent()
		{
			MultipartFormDataContent returnValue = new(String.Format("----------{0:N}", Guid.NewGuid()));
			foreach (KeyValuePair<String, String> keyValuePair in this.ToPairs())
				returnValue.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
			returnValue.Add(this.File.ByteArrayContent(), "file", this.FileName);

			return returnValue;
		}
	}
}
