using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

//https://api.slack.com/reference/block-kit/block-elements
namespace BDMSlackAPI.Messages
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ElementType
	{
		[JsonProperty("button")]
		Button,
		[JsonProperty("checkbox")]
		Checkbox,
		[JsonProperty("datepicker")]
		DatePicker,
		[JsonProperty("image")]
		Image,
		[JsonProperty("multi_static_select")]
		MultiStaticSelect,
		[JsonProperty("multi_external_select")]
		MultiExternalSelect,
		[JsonProperty("multi_users_select")]
		MultiUsersSelect,
		[JsonProperty("multi_conversations_select")]
		MultiConversationsSelect,
		[JsonProperty("multi_channels_select")]
		MultiChannelsSelect,
		[JsonProperty("overflow")]
		Overflow,
		[JsonProperty("plain_text_input")]
		PlainTextInput,
		[JsonProperty("radio_buttons")]
		RadioButtons,
		[JsonProperty("static_select")]
		StaticSelect,
		[JsonProperty("external_select")]
		ExternalSelect,
		[JsonProperty("users_select")]
		UsersSelect,
		[JsonProperty("conversations_select")]
		ConversationsSelect,
		[JsonProperty("channels_select")]
		ChannelsSelect,
		[JsonProperty("timepicker")]
		TimePicker
	}

	public class Element
	{
		public Element() { }

		public Element(ElementType type) => this.Type = type;

		public ElementType Type { get; set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ButtonElementStyle
	{
		[JsonProperty("default")]
		Default,
		[JsonProperty("primary")]
		Primary,
		[JsonProperty("danger")]
		Danger
	}
	public class ButtonElement : Element
	{
		public ButtonElement() : base(ElementType.Button)
		{
			this.Text = new TextCompositionObject(true);
			this.Style = ButtonElementStyle.Default;
		}

		[JsonProperty("text")]
		public TextCompositionObject Text { get; set; }
	
		[JsonProperty("action_id")]
		public String ActionId { get; set; }

		[JsonProperty("url")]
		public String URL { get; set; }

		[JsonProperty("value")]
		public String Value { get; set; }

		[JsonProperty("style")]
		public ButtonElementStyle Style { get; set; }

		[JsonProperty("confirm")]
		public ConfirmCompositionObject Confirm { get; set; }

		[JsonProperty("accessibility_label")]
		public String AccessibilityLabel { get; set; }
	}

	public class CheckboxElement : Element
	{
		public CheckboxElement() : base(ElementType.Checkbox)
		{
			this.Options = new List<OptionCompositionObject>();
		}

		[JsonProperty("action_id")]
		public String ActionId { get; set; }

		[JsonProperty("options")]
		public List<OptionCompositionObject> Options { get; set; }

		[JsonProperty("initial_options")]
		public List<OptionCompositionObject> InitialOptions { get; set; }

		[JsonProperty("confirm")]
		public ConfirmCompositionObject Confirm { get; set; }

		[JsonProperty("focus_on_load")]
		public Boolean FocusOnLoad { get; set; }
	}

	public class DatePickerElement : Element
	{
		public DatePickerElement() : base(ElementType.DatePicker)
		{
			this.Placeholder = new TextCompositionObject(true);
		}

		[JsonProperty("action_id")]
		public String ActionId { get; set; }

		[JsonProperty("placeholder")]
		public TextCompositionObject Placeholder { get; set; }

		[JsonProperty("initial_date"),JsonConverter(typeof(DateConverter))]
		public DateTime? InitialDate { get; set; }

		[JsonProperty("confirm")]
		public ConfirmCompositionObject Confirm { get; set; }

		[JsonProperty("focus_on_load")]
		public Boolean FocusOnLoad { get; set; }
	}

	public class ImageElement : Element
	{
		public ImageElement() : base(ElementType.Image) { }

		[JsonProperty("image_url")]
		public String ImageURL { get; set; }

		[JsonProperty("alt_text")]
		public String AltText { get; set; }
	}

	public class MultiStaticSelectElement : Element
	{
		public MultiStaticSelectElement() : base(ElementType.MultiStaticSelect)
		{
			this.Placeholder = new TextCompositionObject(true);
			this.Options = new List<OptionCompositionObject>();
			this.OptionGroups = new List<OptionGroupCompositionObject>();
		}

		[JsonProperty("action_id")]
		public String ActionId { get; set; }

		[JsonProperty("placeholder")]
		public TextCompositionObject Placeholder { get; set; }

		[JsonProperty("options")]
		public List<OptionCompositionObject> Options { get; set; }

		[JsonProperty("option_groups")]
		public List<OptionGroupCompositionObject> OptionGroups { get; set; }

		[JsonProperty("initial_options")]
		public List<OptionCompositionObject> InitialOptions { get; set; }

		[JsonProperty("confirm")]
		public ConfirmCompositionObject Confirm { get; set; }

		[JsonProperty("focus_on_load")]
		public Boolean FocusOnLoad { get; set; }

		[JsonProperty("max_selected_items")]
		public Int32 MaxSelectedItems { get; set; }
	}

	public class MultiExternalSelectElement : Element
	{
		public MultiExternalSelectElement() : base(ElementType.MultiExternalSelect)
		{
			this.Placeholder = new TextCompositionObject(true);
			this.Options = new List<OptionCompositionObject>();
			this.OptionGroups = new List<OptionGroupCompositionObject>();
		}

		[JsonProperty("action_id")]
		public String ActionId { get; set; }

		[JsonProperty("placeholder")]
		public TextCompositionObject Placeholder { get; set; }

		[JsonProperty("options")]
		public List<OptionCompositionObject> Options { get; set; }

		[JsonProperty("option_groups")]
		public List<OptionGroupCompositionObject> OptionGroups { get; set; }

		[JsonProperty("initial_options")]
		public List<OptionCompositionObject> InitialOptions { get; set; }

		[JsonProperty("confirm")]
		public ConfirmCompositionObject Confirm { get; set; }

		[JsonProperty("focus_on_load")]
		public Boolean FocusOnLoad { get; set; }

		[JsonProperty("max_selected_items")]
		public Int32 MaxSelectedItems { get; set; }

		[JsonProperty("min_query_length")]
		public Int32 MinQueryLength { get; set; }
	}

	public class MultiUsersSelectElement : Element
	{
		public MultiUsersSelectElement() : base(ElementType.MultiUsersSelect)
		{
			this.Placeholder = new TextCompositionObject(true);
		}

		[JsonProperty("action_id")]
		public String ActionId { get; set; }

		[JsonProperty("placeholder")]
		public TextCompositionObject Placeholder { get; set; }

		[JsonProperty("initial_users")]
		public List<String> InitialUSers { get; set; }

		[JsonProperty("confirm")]
		public ConfirmCompositionObject Confirm { get; set; }

		[JsonProperty("focus_on_load")]
		public Boolean FocusOnLoad { get; set; }

		[JsonProperty("max_selected_items")]
		public Int32 MaxSelectedItems { get; set; }
	}
}
