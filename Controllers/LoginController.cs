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
        [Route("/api/login")]
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
        [Route("/api/logout")]
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

        [HttpPost]
        [Route("/api/signup")]
        public async Task<IHttpActionResult> SignUp([FromBody] SignUpRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Datos incompletos");

            if (db.Estudiantes.Any(u => u.Email == request.Email && !u.IsDeleted))
                return BadRequest("Ya existe una cuenta con ese correo.");

            var newStudent = new Students
            {
                StudentName = request.Name,
                StudentLastName = request.LastName,
                StudentEmail = request.Email,
                Password = request.Password,
                StudentPhone = request.Phone,
                StudentAddress = request.Address,
                StudentGender = request.Gender,
                StudentBirthDate = request.BirthDate,
                StudentPhoto = request.Photo,
                IdCampus = request.IdCampus,
                IdUserType = request.IdUserType,
                IdUserState = 2,
                StudentActive = true,
                IsDeleted = false
            };

            db.Estudiantes.Add(newStudent);
            await db.SaveChangesAsync();

            return Ok("Registro exitoso");
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

    public class SignUpRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Photo { get; set; }
        public int IdCampus { get; set; }
        public int IdUserType { get; set; }
    }

}
