using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using PumAssist_API.Models;


namespace PumAssist_API.Controllers
{
    public class LoginController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpPost]
        [Route("/login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest("Credenciales inválidas");

            var student = db.Estudiantes.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password && !u.IsDeleted);
            if (student != null)
            {
                student.IdUserState = 1;
                await db.SaveChangesAsync();
                return Ok(new
                {
                    Tipo = "Estudiante",
                    Usuario = new
                    {
                        student.IdStudent,
                        student.StudentName,
                        student.StudentLastName,
                        student.Email,
                        student.IdUserType,
                        student.IdUserState
                    }
                });
            }

            var teacher = db.Teachers.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password && !u.IsDeleted);
            if (teacher != null)
            {
                teacher.IdUserState = 1;
                await db.SaveChangesAsync();
                return Ok(new
                {
                    Tipo = "Docente",
                    Usuario = new
                    {
                        teacher.IdTeacher,
                        teacher.Name,
                        teacher.LastName,
                        teacher.Email,
                        teacher.IdUserType,
                        teacher.IdUserState
                    }
                });
            }

            return Unauthorized();
        }


        [HttpPost]
        [Route("/logout")]
        public async Task<IHttpActionResult> Logout([FromBody] LogoutRequest logout)
        {
            if (logout == null || string.IsNullOrEmpty(logout.Email))
                return BadRequest("Email requerido");

            var student = db.Estudiantes.FirstOrDefault(u => u.Email == logout.Email && !u.IsDeleted);
            if (student != null)
            {
                student.IdUserState = 2;
                await db.SaveChangesAsync();
                return Ok("Sesión cerrada correctamente del estudiante");
            }

            var teacher = db.Teachers.FirstOrDefault(u => u.Email == logout.Email && !u.IsDeleted);
            if (teacher != null)
            {
                teacher.IdUserState = 2;
                await db.SaveChangesAsync();
                return Ok("Sesión cerrada correctamente del docente");
            }

            return NotFound();
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LogoutRequest
    {
        public string Email { get; set; }
    }

}
