using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BDMSlackAPI.Users
{
	public class UsersAPI
	{
		private readonly Slack _Slack;

		public UsersAPI(Slack slack)
		{
			this._Slack = slack;
		}

		//https://api.slack.com/methods/users.list
		public ListResponse List(ListRequest request)
		{
			return JsonConvert.DeserializeObject<ListResponse>(this._Slack.MakeJsonAPICall("users.list", request));
		}

		///https://api.slack.com/methods/users.lookupByEmail
		public LookupByEmailResponse LookupByEmail(LookupByEmailRequest request)
		{
			return JsonConvert.DeserializeObject<LookupByEmailResponse>(this._Slack.MakeURLEncodedFormAPICall("users.lookupByEmail", request));
		}

		///https://api.slack.com/methods/users.deletePhoto
		///https://api.slack.com/methods/users.identity
		///https://api.slack.com/methods/users.info
		///https://api.slack.com/methods/users.setPhoto
		///https://api.slack.com/methods/users.profile.get
		///https://api.slack.com/methods/users.profile.set
	}
}