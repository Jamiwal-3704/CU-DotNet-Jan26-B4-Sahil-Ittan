using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.Models;
using StudentCourseAPI.DTOs;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        // GET: api/students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(StudentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check unique email
            var exists = await _context.Students.AnyAsync(s => s.Email == dto.Email);
            if (exists)
                return BadRequest("Email already exists");

            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDTO dto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Age = dto.Age;

            await _context.SaveChangesAsync();

            return Ok(student);
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Student deleted");
        }
    }
}