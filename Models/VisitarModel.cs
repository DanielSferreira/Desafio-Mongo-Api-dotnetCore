using System.Collections.Generic;

namespace Models
{
    public class VisitarModel 
    {
        public string _id {get; set;}
        public string Lugar {get; set;}
        public string Descricao {get; set;}
        public int status {get; set;}
        public string[] PontosTuristicos {get; set;}
    }
}