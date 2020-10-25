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
        public async Task<ActionResult<Lugares>> SalvarLugar([FromBody] VisitarModel visitForm)
        {
            var a = new Lugares(visitForm);
            try
            {
                await _lugaresC.InsertOneAsync(a);
            }
            catch (System.Exception)
            {
                return BadRequest();
                throw;
            }
            return Ok(a);
        }

        [HttpGet("listar")]
        public ActionResult PegaLugares()
        {
            try
            {
                var listaLugares = _lugaresC.Find(Builders<Lugares>.Filter.Empty).ToList();
                return Ok(listaLugares);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpGet("listar/{id}")]
        public ActionResult PegaLugaresByLugar(string id)
        {
            try
            {
                var getLugar = _lugaresC.Find(Builders<Lugares>.Filter.Where(e => e._id == id)).First();
                return Ok(getLugar);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult<Lugares>> atualizarUm([FromBody] VisitarModel visitForm)
        {
            var atualizado = new Lugares(visitForm);
            var findi = _lugaresC.Find(Builders<Lugares>.Filter.Where(e => e._id == visitForm._id)).First();
            var filter = Builders<Lugares>.Filter.Eq("_id", findi._id);
            var update = Builders<Lugares>
                .Update
                    .Set("lugar", atualizado.Lugar)
                    .Set("descricao", atualizado.Descricao)
                    .Set("status", atualizado.status)
                    .Set("pontosTuristicos", atualizado.PontosTuristicos);
            try
            {
                await _lugaresC.UpdateOneAsync(filter, update);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
            return Ok(atualizado);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteById(string id)
        {
            try
            {
                var e = await _lugaresC.DeleteOneAsync(Builders<Lugares>.Filter.Where(e => e._id == id));
                return Ok();
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
                throw;
            }
        }
    }
}