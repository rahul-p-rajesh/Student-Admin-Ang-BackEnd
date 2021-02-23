using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/student")]//class route
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LoginContext _context;

        public StudentController(LoginContext context)
        {
            _context = context;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult> GetStudentByUSerName(string userName)
        {
            var student = await _context.Students.Where(x => x.userName == userName).FirstOrDefaultAsync();

            if (student == null)
            {
                return BadRequest(new { message = "student not found" });
            }

            return Ok(student);
        }

        [HttpPut]
        public async Task<ActionResult> putStudentDetatilWithoutPassword(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }


            var existingStudent = _context.Students.Where(s => s.userName == student.userName)
                                                    .FirstOrDefault<Student>();
            if (existingStudent != null)
            {
                existingStudent.firstName = student.firstName;
                existingStudent.lastName = student.lastName;
                existingStudent.email = student.email;
                existingStudent.phoneNumber = student.phoneNumber;
                existingStudent.gender = student.gender;

                await _context.SaveChangesAsync();
                return Ok(new { message = "edit sucessful" });
            }
            else
            {
                return NotFound(new { message = "edit not succesfull" });
            }

        }

        [HttpPut]
        [Route("studentwithpassword")]

        public async Task<ActionResult> putStudentDetatilWithPassword(StudentCreate studentwithallData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            var existingStudent = _context.Students.Where(s => s.userName == studentwithallData.userName).FirstOrDefault<Student>();

            var existingUserStudent = _context.UserLogins.Where(u => u.userName == studentwithallData.userName).FirstOrDefault<UserLogin>();

            if(existingStudent != null && existingUserStudent != null)
            {
                existingStudent.firstName = studentwithallData.firstName;
                existingStudent.lastName = studentwithallData.lastName;
                existingStudent.email = studentwithallData.email;
                existingStudent.phoneNumber = studentwithallData.phoneNumber;
                existingStudent.gender = studentwithallData.gender;

                existingUserStudent.password = studentwithallData.password;

                await _context.SaveChangesAsync();
                return Ok(new { message = "edit sucessful" });
            }
            else
            {
                return NotFound(new { message = "edit not succesfull" });

            }


        }

    }
}