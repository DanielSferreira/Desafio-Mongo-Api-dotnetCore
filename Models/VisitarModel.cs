using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Models
{
    public class VisitarModel 
    {

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id {get; set;}
        public string Lugar {get; set;}
        public string Descricao {get; set;}
        public int status {get; set;}
        public string[] PontosTuristicos {get; set;}
    }
}