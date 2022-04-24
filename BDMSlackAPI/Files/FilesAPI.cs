using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BDMSlackAPI.Files
{
	public class FilesAPI
	{
		private readonly Slack _Slack;

		public FilesAPI(Slack slack)
		{
			this._Slack = slack;
		}

		///https://api.slack.com/methods/files.delete
		///https://api.slack.com/methods/files.info
		///https://api.slack.com/methods/files.list

		//https://api.slack.com/methods/files.upload
		public UploadResponse Upload(UploadRequest request)
		{
			return JsonConvert.DeserializeObject<UploadResponse>(this._Slack.MakeFormAPICall("files.upload", request));
		}
	}
}