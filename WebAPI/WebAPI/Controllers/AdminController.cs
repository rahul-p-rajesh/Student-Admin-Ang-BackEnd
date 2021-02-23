using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]//class route
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly LoginContext _context;

        public AdminController(LoginContext context)
        {
            _context = context;
        }

        [HttpGet]
        //GET api/admin/
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudent()
        {
            var Students = await _context.Students.ToListAsync();
            return Ok(Students);
        }



        // GET: api/Admin/username
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                return Ok(student);
            }

            return NotFound();
        }


        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStudentDetail(int id, Student student)
        {

            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var studentExist = await _context.Students.FindAsync(student.Id);
                if (studentExist == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        // studentWithAllData will contain userName, firstName, lastName, password

        public async Task<ActionResult<Student>> PostStudent(StudentCreate studentWithAllData)
        {
            var studentExist = await _context.Students.Where(
                x => x.userName == studentWithAllData.userName).ToListAsync();

            if (studentExist.Any())
            {
                return BadRequest(new { message = "student already exist" });
            }
            if (studentWithAllData.userName == "" || studentWithAllData.firstName == "" ||
                studentWithAllData.lastName == "" || studentWithAllData.password == "")
            {
                return BadRequest(new { message = "username, first name, last name or password in wrong format" });
            }

            Student student = new Student();
            student.userName = studentWithAllData.userName;
            student.firstName = studentWithAllData.firstName;
            student.lastName = studentWithAllData.lastName;
            student.email = studentWithAllData.email;
            student.phoneNumber = studentWithAllData.phoneNumber;
            student.gender = studentWithAllData.gender;

            UserLogin user = new UserLogin();
            user.userName = studentWithAllData.userName;
            user.password = studentWithAllData.password;
            user.userType = "student";

            _context.Students.Add(student);
            _context.UserLogins.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentById", new { id = student.Id }, student);
        }
        //need to be resolved
        // DELETE: api/Admin/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {

            var student = _context.Students.Find(id);
            var studentUserLogin = await _context.UserLogins.Where(x => x.userName == student.userName).FirstOrDefaultAsync();

            if (student == null || studentUserLogin == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.UserLogins.Remove(studentUserLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool StudentDetailExists(string userName)
        {
            return _context.Students.Where(
                x => x.userName == userName).First() != null;
        }

    }
}
