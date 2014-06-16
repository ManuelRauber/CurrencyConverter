using System;
using Newtonsoft.Json;

namespace TecDemo.Mobile.Text
{
	public class ConcreteTypeConverter<TConcrete> : Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize<TConcrete>(reader);
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(TConcrete) == objectType;
		}
	}
}