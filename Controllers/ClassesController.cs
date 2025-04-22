using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PumAssist_API.Models;

namespace PumAssist_API.Controllers
{
    public class ClassesController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpGet]
        [Route("api/Classes/Get")]
        public async Task<IEnumerable<Models.Classes>> Get()
        {
            return await db.Classes.ToListAsync();
        }

        [HttpGet]
        [Route("api/Classes/Get/{id}")]
        public async Task<Models.Classes> GetbyId(int id)
        {
            return await db.Classes.FindAsync(id); 
        }

        [HttpGet]
        [Route("api/Classes/GetByAlumn/{idAlumno}")]
        public async Task<IEnumerable<Models.Classes>> GetClassesByAlumn(int idAlumno)
        {
            return await db.Classes.Where(c => c.StudentsList.Any(s => s.IdStudent == idAlumno)).ToListAsync(); 
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Models.Classes classes)
        {
            db.Classes.Add(classes);
            await db.SaveChangesAsync(); 
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Classes classes)
        {
            db.Entry(classes).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync(); 
            return Ok();
        }

        [HttpPut]
        [Route("api/Classes/Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.Classes classs = await db.Classes.FindAsync(id); 
                classs.IsDeleted = true;
                db.Entry(classs).State = System.Data.Entity.EntityState.Modified;
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
