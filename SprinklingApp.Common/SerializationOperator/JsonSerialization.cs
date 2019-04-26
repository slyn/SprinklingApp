using System;
using Newtonsoft.Json;

namespace SprinklingApp.Common.SerializationOperator {

    public class JsonSerialization : ISerialization {
        public object Serialize(object data) {
            string returnStr = JsonConvert.SerializeObject(data);
            return returnStr;
        }

        public object Deserialize<T>(object data) {
            T result = JsonConvert.DeserializeObject<T>(data.ToString());
            return result;
        }

        public object Deserialize(Type type, object data) {
            object result = JsonConvert.DeserializeObject(data.ToString(), type);
            return result;
        }
    }

}