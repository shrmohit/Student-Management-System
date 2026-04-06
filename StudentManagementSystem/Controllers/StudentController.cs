using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;
using StudentManagementSystem.DTOs.Student;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _service;

        public StudentController(IStudentService service, ILogger<StudentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all students");
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentDto addstudent)
        {
            
            var student = new Student
            {
                Name = addstudent.Name,
                Age = addstudent.Age,
                Email = addstudent.Email,
                Course = addstudent.Course,
            };
            await _service.Add(student);
            return Ok("Student added Successfully");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update( int id , UpdateStudentDto updatestudent)
        {
            var student = await _service.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = updatestudent.Name;
            student.Age = updatestudent.Age;
            student.Email = updatestudent.Email;
            student.Course = updatestudent.Course;
           
            await _service.Update(student);
            return Ok("Updated Successfully");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
