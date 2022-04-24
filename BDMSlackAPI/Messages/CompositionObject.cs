using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

//https://api.slack.com/reference/block-kit/composition-objects
namespace BDMSlackAPI.Messages
{
	public enum CompositionObjectType
	{
		Text,
		Confirm,
		Option,
		OptionGroup,
		Dispatch,
		Filter
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum TextType
	{
		[JsonProperty("mrkdwn")]
		Markdown,

		[JsonProperty("plain_text")]
		PlainText
	}

	public class CompositionObject
	{
		public CompositionObject() { }

		public CompositionObject(CompositionObjectType type) => this.Type = type;

		[JsonIgnore()]
		public CompositionObjectType Type { get; set; }
	}

	public class TextCompositionObject : CompositionObject
	{
		private readonly Boolean _MustBePlainText = false;
		public TextCompositionObject() : base(CompositionObjectType.Text)
		{
			this._TextType = TextType.Markdown;
		}

		public TextCompositionObject(Boolean mustBePlainText) : base(CompositionObjectType.Text)
		{
			this._MustBePlainText = mustBePlainText;
			this._TextType = TextType.PlainText;
		}

		private TextType _TextType;
		[JsonProperty("type")]
		public TextType TextType
		{
			get
			{
				return this._TextType;
			}
			set
			{
				if (this._MustBePlainText && (value != TextType.PlainText))
					throw new ArgumentException("TextType must be plain text for this specific type of element or block.");
				this._TextType = value;
			}
		}

		[JsonProperty("text")]
		public String Text { get; set; }

		[JsonProperty("emoji")]
		public Boolean Emoji { get; set; }

		[JsonProperty("verbatim")]
		public Boolean Verbatim { get; set; }
	}

	public class ConfirmCompositionObject : CompositionObject
	{
		public ConfirmCompositionObject() : base(CompositionObjectType.Confirm)
		{
			this.Title = new TextCompositionObject(true);
			this.Text = new TextCompositionObject();
			this.Confirm = new TextCompositionObject(true);
			this.Deny = new TextCompositionObject(true);
		}

		[JsonProperty("title")]
		public TextCompositionObject Title { get; set; }

		[JsonProperty("text")]
		public TextCompositionObject Text { get; set; }

		[JsonProperty("confirm")]
		public TextCompositionObject Confirm { get; set; }

		[JsonProperty("deny")]
		public TextCompositionObject Deny { get; set; }

		[JsonProperty("style")]
		public String Style { get; set; }
	}

	public class OptionCompositionObject : CompositionObject
	{
		public OptionCompositionObject() : base(CompositionObjectType.Option)
		{
			this.Text = new TextCompositionObject();
			this.Description = new TextCompositionObject(true);
		}

		public OptionCompositionObject(ElementType parentElementType) : base(CompositionObjectType.Option)
		{
            this.Text = parentElementType switch
            {
                ElementType.MultiStaticSelect or ElementType.MultiExternalSelect or ElementType.MultiUsersSelect or ElementType.MultiConversationsSelect or ElementType.MultiChannelsSelect or ElementType.Overflow or ElementType.StaticSelect or ElementType.ExternalSelect or ElementType.UsersSelect or ElementType.ConversationsSelect or ElementType.ChannelsSelect => new TextCompositionObject(true),
                _ => new TextCompositionObject(),
            };
            this.Description = new TextCompositionObject(true);
		}

		[JsonProperty("text")]
		public TextCompositionObject Text { get; set; }

		[JsonProperty("value")]
		public String Value { get; set; }

		[JsonProperty("description")]
		public TextCompositionObject Description { get; set; }

		[JsonProperty("url")]
		public String URL { get; set; }
	}

	public class OptionGroupCompositionObject : CompositionObject
	{
		public OptionGroupCompositionObject() : base(CompositionObjectType.OptionGroup)
		{
			this.Label = new TextCompositionObject(true);
			this.Options = new List<OptionCompositionObject>();
		}

		[JsonProperty("label")]
		public TextCompositionObject Label { get; set; }

		[JsonProperty("options")]
		public List<OptionCompositionObject> Options { get; set; }
	}

	[Flags]
	public enum DispatchTriggerAction
	{
		OnEnterPressed,
		OnCharacterEntered
	}
	[JsonConverter(typeof(DispatchActionConfigCompositionObjectConverter))]
	public class DispatchActionConfigCompositionObject : CompositionObject
	{
		public DispatchActionConfigCompositionObject() : base(CompositionObjectType.Dispatch)
		{
			this.TriggerActionsOn = DispatchTriggerAction.OnEnterPressed;
		}

		public DispatchTriggerAction TriggerActionsOn { get; set; }
	}
	public class DispatchActionConfigCompositionObjectConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DispatchActionConfigCompositionObject dispatch = value as DispatchActionConfigCompositionObject;
			writer.WriteStartObject();
			writer.WriteStartArray();
			if ((dispatch.TriggerActionsOn & DispatchTriggerAction.OnEnterPressed) == DispatchTriggerAction.OnEnterPressed)
				writer.WriteValue("on_enter_pressed");
			if ((dispatch.TriggerActionsOn & DispatchTriggerAction.OnCharacterEntered) == DispatchTriggerAction.OnCharacterEntered)
				writer.WriteValue("on_character_entered");
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(DispatchActionConfigCompositionObject).IsAssignableFrom(objectType);
		}
	}

	[Flags]
	public enum FilterInclude
	{
		PublicConversations = 0,
		PrivateConversations = 1,
		MultiPersonIMs = 2,
		IMs = 4
	}
	[JsonConverter(typeof(FilterCompositionObjectConverter))]
	public class FilterCompositionObject : CompositionObject
	{
		public FilterCompositionObject() : base(CompositionObjectType.Filter)
		{
			this.Include = FilterInclude.PublicConversations
				| FilterInclude.PrivateConversations
				| FilterInclude.MultiPersonIMs
				| FilterInclude.IMs;
			this.ExcludeExternalSharedChannels = false;
			this.ExcludeBotUsers = false;
		}

		public FilterInclude Include { get; set; }
		public Boolean ExcludeExternalSharedChannels { get; set; }
		public Boolean ExcludeBotUsers { get; set; }
	}
	public class FilterCompositionObjectConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			FilterCompositionObject filter = value as FilterCompositionObject;
			writer.WriteStartObject();
			writer.WriteStartArray();
			if ((filter.Include & FilterInclude.PublicConversations) == FilterInclude.PublicConversations)
				writer.WriteValue("public");
			if ((filter.Include & FilterInclude.PrivateConversations) == FilterInclude.PrivateConversations)
				writer.WriteValue("private");
			if ((filter.Include & FilterInclude.MultiPersonIMs) == FilterInclude.MultiPersonIMs)
				writer.WriteValue("mpim");
			if ((filter.Include & FilterInclude.IMs) == FilterInclude.IMs)
				writer.WriteValue("im");
			writer.WriteEndArray();
			writer.WriteBooleanProperty(serializer, "exclude_external_shared_channels", filter.ExcludeExternalSharedChannels);
			writer.WriteBooleanProperty(serializer, "exclude_bot_users", filter.ExcludeBotUsers);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(DispatchActionConfigCompositionObject).IsAssignableFrom(objectType);
		}
	}
}
