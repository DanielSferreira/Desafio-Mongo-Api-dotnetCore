using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using MongoDB.Driver;
using Data.Collections;
using System.Threading.Tasks;
using static System.Console;

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
        public async Task<ActionResult> SalvarLugar([FromBody] VisitarModel visitForm)
        { 
            var a = new Lugares(visitForm);
            try
            {
                await _lugaresC.InsertOneAsync(a);
            }
            catch (System.Exception)
            {
                return StatusCode(501, "Problemas na inserção de dados");
                throw;
            }
            return StatusCode(201, a.Lugar + " Inserido com successo");
        }

        [HttpGet("listar")]
        public ActionResult PegaLugares()
        {
            var infectados = _lugaresC.Find(Builders<Lugares>.Filter.Empty).ToList();
            return Ok(infectados);
        }
    }
}