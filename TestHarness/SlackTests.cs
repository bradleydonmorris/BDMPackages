using BDMSlackAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness
{
    public class K9SlackAPI
    {
		private readonly Slack _Slack;

		public K9SlackAPI(String token)
        {
			this._Slack = new Slack(token);
		}

		public void CreateMissionChannel(String name, String localMissionDirectory)
		{
			List<BDMSlackAPI.Files.UploadRequest> templateFiles = new();
			List<BDMSlackAPI.Users.Member> ownerAdminMembers = this.GetOwnersAndAdmins();
			List<String> ownersAndAdmins = new();
			if (ownerAdminMembers is not null)
				ownersAndAdmins = ownerAdminMembers
					.Select(m => m.Id).ToList();
			if (Directory.Exists(localMissionDirectory))
				foreach (String templateName in new String[] { "502", "214", "309" })
				{
					String filePath = Path.Combine(localMissionDirectory, $"{templateName}.xlsx");
					if (File.Exists(filePath))
						templateFiles.Add
						(
							BDMSlackAPI.Files.UploadRequest.BuildFromFile
							(
									filePath,
									$"{templateName} Template",
									$"Data entry template for {templateName} form."
							)
						);
				}
			this.CreateChannel(name, true, "Callout", "This is an official part of the search documentation and may be subpoenaed by court. Please conduct accordingly.", ownersAndAdmins, templateFiles);
		}

		public List<BDMSlackAPI.Users.Member> GetOwnersAndAdmins()
		{
			List<BDMSlackAPI.Users.Member> returnValue;
			BDMSlackAPI.Users.ListResponse listResponse = this._Slack.Users.List(new());
			if (!listResponse.Ok)
				throw new Exception("Unable to get list of users");
			returnValue = listResponse.Members
					.FindAll(m => m.IsAdmin || m.IsOwner);
			return returnValue;
		}

		public void CreateChannel(String name, Boolean isPrivate, String topic, String purpose, List<String> usersToInvite, List<BDMSlackAPI.Files.UploadRequest> filesToPost)
		{
			BDMSlackAPI.Conversations.CreateResponse createResponse = this._Slack.Conversations.Create
			(
				new BDMSlackAPI.Conversations.CreateRequest
				{
					IsPrivate = isPrivate,
					Name = name
				}
			);
			if (!createResponse.Ok)
				throw new Exception("Channel not created");
			BDMSlackAPI.Conversations.SetTopicResponse setTopicResponse = new() { Ok = false };
			if (!String.IsNullOrEmpty(topic))
				setTopicResponse = this._Slack.Conversations.SetTopic
				(
					new BDMSlackAPI.Conversations.SetTopicRequest()
					{
						Channel = createResponse.Conversation.Id,
						Topic = topic
					}
				);
			if (!setTopicResponse.Ok)
				throw new Exception("Unable to set purpose");
			BDMSlackAPI.Conversations.SetPurposeResponse setPurposeResponse = this._Slack.Conversations.SetPurpose
			(
				new BDMSlackAPI.Conversations.SetPurposeRequest()
				{
					Channel = createResponse.Conversation.Id,
					Purpose = purpose
				}
			);
			if (!setPurposeResponse.Ok)
				throw new Exception("Unable to set purpose");

			if (usersToInvite is not null && usersToInvite.Count > 0)
			{
				BDMSlackAPI.Conversations.InviteResponse inviteResponse = this._Slack.Conversations.Invite
				(
					new BDMSlackAPI.Conversations.InviteRequest()
					{
						Channel = createResponse.Conversation.Id,
						Users = usersToInvite
					}
				);
				if (!inviteResponse.Ok)
					throw new Exception("Unable to invite owers and admins");
			}
			if (filesToPost is not null && filesToPost.Count > 0)
			{
				List<String> channels = new(new String[] { createResponse.Conversation.Id });
				foreach (BDMSlackAPI.Files.UploadRequest uploadRequest in filesToPost)
				{
					uploadRequest.Channels = channels;
					BDMSlackAPI.Files.UploadResponse uploadResponse = this._Slack.Files.Upload(uploadRequest);
					if (!uploadResponse.Ok)
						throw new Exception(String.Format("Unable to upload {0}", uploadRequest.FileName));
				}
			}
		}
	}
}
