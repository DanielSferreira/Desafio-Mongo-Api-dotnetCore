using Models;

namespace Data.Collections
{
    public class Lugares
    {
        public Lugares(VisitarModel a) => _visitarModel = a;
        
        public VisitarModel _visitarModel {get; set;}
    }
}