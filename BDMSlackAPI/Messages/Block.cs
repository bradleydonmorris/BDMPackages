using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI.Messages
{

	[JsonConverter(typeof(StringEnumConverter))]
	public enum BlockType
	{
		[JsonProperty("divider")]
		Divider,

		[JsonProperty("context")]
		Context,

		[JsonProperty("header")]
		Header,

		[JsonProperty("image")]
		Image,

		[JsonProperty("section")]
		Section,
	}

	public class Block
	{
		public Block() { }

		public Block(BlockType type) => this.Type = type;

		[JsonProperty("type")]
		public BlockType Type { get; set; }

		[JsonProperty("block_id")]
		public String BlockId { get; set; }
	}

	public class ContextBlock : Block
	{
		public ContextBlock() : base(BlockType.Context) { }

		[JsonProperty("elements")]
		public List<Element> Elements { get; set; }
	}

	public class DividerBlock : Block
	{
		public DividerBlock() : base(BlockType.Divider) { }
	}

	public class HeaderBlock : Block
	{
		public HeaderBlock() : base(BlockType.Header)
		{
			this.Text = new TextCompositionObject(true);
		}

		[JsonProperty("text")]
		public TextCompositionObject Text { get; set; }
	}

	public class ImageBlock : Block
	{
		public ImageBlock() : base(BlockType.Image)
		{
			this.Title = new TextCompositionObject(true);
		}

		[JsonProperty("image_url")]
		public String ImageURL { get; set; }

		[JsonProperty("alt_text")]
		public String AltText { get; set; }

		[JsonProperty("title")]
		public TextCompositionObject Title { get; set; }
	}

	public class SectionBlock : Block
	{
		public SectionBlock() : base(BlockType.Section) { }
		public SectionBlock(String markdown) : base(BlockType.Section)
		{
			this.Text = new TextCompositionObject();
			this.Fields = new List<CompositionObject>();
		}

		[JsonProperty("text")]
		public TextCompositionObject Text { get; set; }

		[JsonProperty("accessory")]
		public Element Accessory { get; set; }

		[JsonProperty("fields")]
		public List<CompositionObject> Fields { get; set; }
	}
}
