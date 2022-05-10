using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMJsonConverters
{
	public class DateTimeConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is DateTime?)
			{
				DateTime? dateTimeValue = value as DateTime?;
				if (dateTimeValue.HasValue)
					writer.WriteValue(dateTimeValue.Value.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DateTime? returnValue = null;
			if (reader.TokenType == JsonToken.String)
				returnValue = DateTime.Parse(reader.Value as String);
			return returnValue;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(DateTime?).IsAssignableFrom(objectType);
		}
	}
}