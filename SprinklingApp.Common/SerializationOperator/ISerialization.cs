using System;

namespace SprinklingApp.Common.SerializationOperator {

    public interface ISerialization {
        object Serialize(object data);
        object Deserialize<T>(object data);
        object Deserialize(Type type, object data);
    }

}