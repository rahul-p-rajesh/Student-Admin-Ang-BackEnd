using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]//class route
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LoginContext _context;

        public UserController(LoginContext context)
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

    }
}
