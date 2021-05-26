using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ServerDataBase
{
    public class Article
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
