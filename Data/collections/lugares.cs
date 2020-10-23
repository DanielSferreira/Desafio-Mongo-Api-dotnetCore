using System.Collections.Generic;
using Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Data.Collections
{
    public class Lugares
    {
        public Lugares(VisitarModel a)
        {
            Lugar = a.Lugar;
            Descricao = a.Descricao;
            status = a.status;
            PontosTuristicos = a.PontosTuristicos;
        }
        public Lugares(
            string _Lugar,
            string _Descricao,
            int _status,
            string[] _PontosTuristicos
        )
        {
            Lugar = _Lugar;
            Descricao = _Descricao;
            status = _status;
            PontosTuristicos = _PontosTuristicos;
        }
        public Lugares()
            { }

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Lugar { get; set; }
        public string Descricao { get; set; }
        public int status { get; set; }
        public string[] PontosTuristicos { get; set; }
    }
}