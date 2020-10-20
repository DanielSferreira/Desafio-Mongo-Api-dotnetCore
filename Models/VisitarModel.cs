using System.Collections.Generic;

namespace Models
{
    public class VisitarModel 
    {
        public string Lugar {get; set;}
        public string Descricao {get; set;}
        public int status {get; set;}
        public List<string> PontosTuristicos {get; set;}
    }
}