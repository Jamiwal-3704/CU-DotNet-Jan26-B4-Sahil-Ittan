using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.Models;
using StudentCourseAPI.DTOs;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        // GET: api/courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(CourseDTO dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Credits = dto.Credits
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDTO dto)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            course.Title = dto.Title;
            course.Credits = dto.Credits;

            await _context.SaveChangesAsync();

            return Ok(course);
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok("Course deleted");
        }
    }
}