using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMSlackAPI
{
	public static class Extensions
	{
		public static void WriteStringProperty(this JsonWriter writer, JsonSerializer serializer, String name, String value, Boolean includeNull = false)
		{
			if (!String.IsNullOrEmpty(value))
			{
				writer.WritePropertyName(name);
				serializer.Serialize(writer, value);
			}
			else if (includeNull)
			{
				writer.WritePropertyName(name);
				serializer.Serialize(writer, null);
			}
		}

		public static void WriteBooleanProperty(this JsonWriter writer, JsonSerializer serializer, String name, Boolean value)
		{
			writer.WritePropertyName(name);
			serializer.Serialize(writer, value.ToString().ToLower());
		}

		public static void WriteInt32Property(this JsonWriter writer, JsonSerializer serializer, String name, Int32 value)
		{
			writer.WritePropertyName(name);
			serializer.Serialize(writer, value.ToString());
		}

		public static void WriteInt64Property(this JsonWriter writer, JsonSerializer serializer, String name, Int64 value)
		{
			writer.WritePropertyName(name);
			serializer.Serialize(writer, value.ToString());
		}

		public static DateTime ToUnixTime(this Int64 seconds)
		{
			return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(seconds);
		}
	}
}
