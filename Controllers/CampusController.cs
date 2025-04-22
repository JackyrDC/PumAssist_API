using PumAssist_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PumAssist_API.Controllers
{
    public class CampusController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [HttpGet]
        [Route("api/Campus/Get")]
        public async Task<IEnumerable<Models.Campus>> Get()
        {
            try { 
            return await db.Campus.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar acceder al listado de campus: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("api/Campus/Get/{id}")]
        public async Task<Models.Campus> Get(int id)
        {
            return await db.Campus.FindAsync(id);
        }
        [HttpPost]
        [Route("api/Campus/GetByAlumn/{idAlumno}")]
        public async Task<IHttpActionResult> Post([FromBody] Models.Campus campus)
        {
            db.Campus.Add(campus);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("api/Campus/Put/{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Models.Campus campus)
        {
            db.Entry(campus).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("api/Campus/Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.Campus campus = await db.Campus.FindAsync(id);
                campus.IsDeleted = true;
                db.Entry(campus).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
