using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Files
{
	public class UploadRequestConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			UploadRequest request = value as UploadRequest;
			writer.WriteStartObject();
			writer.WriteStringProperty(serializer, "token", request.Token, true);
			writer.WriteInt32Property(serializer, "pretty", request.Pretty);
			writer.WriteStringProperty(serializer, "filename", request.FileName);
			writer.WriteStringProperty(serializer, "filetype", request.FileType);
			writer.WriteStringProperty(serializer, "title", request.Title);
			writer.WriteStringProperty(serializer, "channels", String.Join(",", request.Channels));
			writer.WriteStringProperty(serializer, "thread_ts", request.ThreadTS);
			writer.WritePropertyName("file");
			writer.WriteStartObject();
			writer.WriteStringProperty(serializer, "FileName", request.File.FileName);
			writer.WriteStringProperty(serializer, "ContentType", request.File.ContentType);
			writer.WriteStringProperty(serializer, "Contents", Convert.ToBase64String(request.File.Contents));
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(UploadRequest).IsAssignableFrom(objectType);
		}
	}
}
