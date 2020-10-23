using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using MongoDB.Driver;
using Data.Collections;
using System.Threading.Tasks;
using System.Linq;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("listar/{str}")]
        public ActionResult PegaLugaresByLugar(string str)
        {
            var infectados = _lugaresC.Find(Builders<Lugares>.Filter.Where(e => e.Lugar == str)).First();
            return Ok(infectados);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult> atualizarUm([FromBody] VisitarModel visitForm)
        {
            var a = new Lugares(visitForm);
            var filter = Builders<Lugares>.Filter.Eq("_id", a.Lugar);
            var update = Builders<Lugares>.Update.Set("_id", a.Lugar);
            try
            {
                await _lugaresC.UpdateOneAsync(filter, update);
            }
            catch (System.Exception)
            {
                return StatusCode(501, "Problemas na inserção de dados");
                throw;
            }
             return StatusCode(203, a.Lugar + " Alterado com successo");
        }
    }
}