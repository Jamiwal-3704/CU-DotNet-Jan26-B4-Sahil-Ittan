using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/enrollment
        [HttpPost]
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);

            if (!studentExists || !courseExists)
                return BadRequest("Invalid StudentId or CourseId");

            var alreadyEnrolled = await _context.StudentCourses
                .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (alreadyEnrolled)
                return BadRequest("Student already enrolled in this course");

            var enrollment = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.StudentCourses.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok("Enrolled successfully");
        }
    }
}