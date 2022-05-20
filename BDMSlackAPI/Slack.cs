using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BDMSlackAPI
{
	public class Slack
	{
		public Chat.ChatAPI Chat { get; set; }
		public Conversations.ConversationsAPI Conversations { get; set; }
		public Users.UsersAPI Users { get; set; }
		public Files.FilesAPI Files { get; set; }

        private readonly HttpClient _HttpClient = new();

		public Slack(String token)
		{
			this._Token = token;
			this.Conversations = new Conversations.ConversationsAPI(this);
			this.Chat = new Chat.ChatAPI(this);
			this.Users = new Users.UsersAPI(this);
			this.Files = new Files.FilesAPI(this);
			this._HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._Token);
		}

		private readonly String _Token;

		internal String MakeJsonAPICall(String api, RequestBase request)
		{
			String returnValue = String.Empty;
			request.Token = this._Token;
			HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, String.Format("https://slack.com/api/{0}", api));
			httpRequestMessage.Content = request.StringContent();
			this._HttpClient.SendAsync(httpRequestMessage).ContinueWith(response =>
				returnValue = response.Result.Content.ReadAsStringAsync().Result
			);
			return returnValue;
		}

		internal String MakeURLEncodedFormAPICall(String api, RequestBase request)
		{
			String returnValue = String.Empty;
			request.Token = this._Token;
			HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, String.Format("https://slack.com/api/{0}", api));
			httpRequestMessage.Content = request.FormUrlEncodedContent();
			this._HttpClient.SendAsync(httpRequestMessage).ContinueWith(response =>
				returnValue = response.Result.Content.ReadAsStringAsync().Result
			);
			return returnValue;
		}

		internal String MakeFormAPICall(String api, RequestBase request)
		{
			String returnValue = String.Empty;
			request.Token = this._Token;
			HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, String.Format("https://slack.com/api/{0}", api));
			httpRequestMessage.Content = request.MultipartFormDataContent();
			this._HttpClient.SendAsync(httpRequestMessage).ContinueWith(response =>
				returnValue = response.Result.Content.ReadAsStringAsync().Result
			);
			return returnValue;
		}
	}
}
