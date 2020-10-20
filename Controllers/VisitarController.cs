using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using MongoDB.Driver;
using Data.Collections;
//using static System.Console;
namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitarController : ControllerBase
    {

        private MongoDb _mongoDb;
        IMongoCollection<Lugares> _lugaresC;
        
        public VisitarController(MongoDb mDb)
        {
            _mongoDb = mDb;
            _lugaresC = _mongoDb.DB.GetCollection<Lugares>("lugares");
        }
        [HttpPost("salvarLugar")]
        public ActionResult SalvarLugar([FromBody] VisitarModel visitForm)
        {
            var l = new Lugares(visitForm);
            _lugaresC.InsertOne(l);
            return StatusCode(201,visitForm.Lugar+" Inserido com successo");
        }

        [HttpGet]
        public ActionResult PegaLugares()
        {
            var infectados = _lugaresC.Find(Builders<Lugares>.Filter.Empty).ToList();
            return Ok(infectados);
        }
    }
}