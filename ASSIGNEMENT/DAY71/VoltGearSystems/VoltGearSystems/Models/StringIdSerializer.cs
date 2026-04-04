//namespace VoltGearSystems.Models
//{
//    internal class StringIdSerializer
//    {
//    }
//}

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace VoltGearSystems.Serializers
{
    public class StringIdSerializer : SerializerBase<string?>
    {
        public override string? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            var bsonType = reader.GetCurrentBsonType();
            return bsonType switch
            {
            BsonType.String => reader.ReadString(),
            BsonType.ObjectId => reader.ReadObjectId().ToString(),
            BsonType.Int32 => reader.ReadInt32().ToString(),
            BsonType.Int64 => reader.ReadInt64().ToString(),
            BsonType.Null => { reader.ReadNull(); return null;
        },
                _ => throw new FormatException($"Cannot deserialize BsonType {bsonType} to string")
            };
}

public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string? value)
{
    var writer = context.Writer;

    if (value is null)
    {
        writer.WriteNull();
        return;
    }

    // Prefer writing ObjectId if value parses as one
    if (ObjectId.TryParse(value, out var oid))
    {
        writer.WriteObjectId(oid);
        return;
    }

    // If value is integer-like, write as Int32/Int64
    if (long.TryParse(value, out var longVal))
    {
        if (longVal >= int.MinValue && longVal <= int.MaxValue)
            writer.WriteInt32((int)longVal);
        else
            writer.WriteInt64(longVal);
        return;
    }

    // Default to string
    writer.WriteString(value);
}
    }
}