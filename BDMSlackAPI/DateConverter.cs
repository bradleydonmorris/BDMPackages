using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI
{
	public class DateConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DateTime? dateValue = value as DateTime?;
			if (!dateValue.HasValue)
			{
				writer.WriteValue(dateValue.Value.ToString("yyyy-MM-dd"));
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(DateTime?).IsAssignableFrom(objectType);
		}
	}
}