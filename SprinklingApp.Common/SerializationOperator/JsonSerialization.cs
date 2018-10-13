using Newtonsoft.Json;
using System;

namespace SprinklingApp.Common.SerializationOperator
{
    public class JsonSerialization: ISerialization
    {
        public object Serialize(object data)
        {
            var returnStr = JsonConvert.SerializeObject(data);
            return returnStr.ToString();
        }

        public object Deserialize<T>(object data)
        {
            var result = JsonConvert.DeserializeObject<T>(data.ToString());
            return result;
        }

        public object Deserialize(Type type, object data)
        {
            var result = JsonConvert.DeserializeObject(data.ToString(), type);
            return result;

        }
    }
}
