using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BDMSlackAPI
{
	public class Slack
	{
		public Chat.ChatAPI Chat { get; set; }
		public Conversations.ConversationsAPI Conversations { get; set; }
		public Users.UsersAPI Users { get; set; }
		public Files.FilesAPI Files { get; set; }

		public Slack(String token)
		{
			this._Token = token;
			this._Authorization = String.Format("Bearer {0}", this._Token);
			this.Conversations = new Conversations.ConversationsAPI(this);
			this.Chat = new Chat.ChatAPI(this);
			this.Users = new Users.UsersAPI(this);
			this.Files = new Files.FilesAPI(this);
		}

		private readonly String _Token;
		private readonly String _Authorization;

		internal String MakeJsonAPICall(String api, RequestBase request)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format("https://slack.com/api/{0}", api));
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers.Add("Authorization", this._Authorization);
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			request.Token = this._Token;
			String jsonBody = JsonConvert.SerializeObject(request);
			using (StreamWriter streamWriter = new(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(jsonBody);
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			String responseBody;
			using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
			{
				responseBody = streamReader.ReadToEnd();
			}
			return responseBody;
		}

		internal String MakeURLEncodedFormAPICall(String api, RequestBase request)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format("https://slack.com/api/{0}", api));
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers.Add("Authorization", this._Authorization);
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			request.Token = this._Token;
			String formBody = request.FormURLEncode();
			using (StreamWriter streamWriter = new(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(formBody);
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			String responseBody;
			using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
			{
				responseBody = streamReader.ReadToEnd();
			}
			return responseBody;
		}

		internal String MakeFormAPICall(String api, RequestBase request)
		{
			String formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format("https://slack.com/api/{0}", api));
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers.Add("Authorization", this._Authorization);
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + formDataBoundary;
			request.Token = this._Token;
			Byte[] formBody = request.FormBodyEncode(formDataBoundary);
			httpWebRequest.ContentLength = formBody.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(formBody, 0, formBody.Length);
				requestStream.Close();
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			String responseBody;
			using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
			{
				responseBody = streamReader.ReadToEnd();
			}
			return responseBody;
		}
	}
}
