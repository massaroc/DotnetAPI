using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetAPI
{
    public class Music
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string[] TrackList { get; set; }
    }

}