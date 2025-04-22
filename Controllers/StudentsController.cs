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
    public class StudentsController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpGet]
        [Route("api/Students/GET")]
        public async Task<IEnumerable<Models.Students>> Get()
        {
            try
            {
                return await db.Estudiantes.ToListAsync();
            }
            catch
            {
                return (IEnumerable<Students>)BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Students/GET/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var student = await db.Estudiantes.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("api/Students/POST")]
        public async Task<IHttpActionResult> Post([FromBody] Models.Students Student)
        {
            try
            {
                db.Estudiantes.Add(Student);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                Console.WriteLine("Error en la creación del nuevo estudiante");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Students/PostMany")]
        public async Task<IHttpActionResult> PostMany([FromBody] Models.Students[] students)
        {
            try
            {
                db.Estudiantes.AddRange(students);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Students/PUT/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Students student)
        {
            try
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Students/DELETE/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.Students student = await db.Estudiantes.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                student.IsDeleted = true;
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
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