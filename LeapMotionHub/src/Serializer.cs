using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Leap;
using MsgPack.Serialization;

namespace LeapMotionHub
{
	public static class Serializer
	{
		public static byte[] serializeFrame(Frame frame)
		{
			var serializer = SerializationContext.Default.GetSerializer<Frame>();
			MemoryStream stream = new MemoryStream();

			serializer.Pack(stream, frame);

			return stream.ToArray();
		}

		public static Frame deserializeFrame(byte[] bytes)
		{
			var serializer = SerializationContext.Default.GetSerializer<Frame>();
			MemoryStream stream = new MemoryStream(bytes);

			return serializer.Unpack(stream) as Frame;
		}

		public static string Serialize<T>(this T obj)
		{
			var serializer = new DataContractSerializer(obj.GetType());
			using (var writer = new StringWriter())
			using (var stm = new XmlTextWriter(writer))
			{
				serializer.WriteObject(stm, obj);
				return writer.ToString();
			}
		}

		public static T Deserialize<T>(this string serialized)
		{
			var serializer = new DataContractSerializer(typeof(T));
			using (var reader = new StringReader(serialized))
			using (var stm = new XmlTextReader(reader))
			{
				return (T)serializer.ReadObject(stm);
			}
		}
	}
}
