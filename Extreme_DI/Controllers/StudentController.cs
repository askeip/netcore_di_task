using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Extreme_DI.Models;
using Extreme_DI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Extreme_DI.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository studentRepository { get; set; }

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public IActionResult All()
        {
            return View(studentRepository.GetStudents());
        }

        [HttpGet]
        public IActionResult Profile(int studentID, [FromServices]IStudentRepository studentRepository)
        {
            var student = studentRepository.GetStudentModel(studentID);
            if (student != null)
                return View(student);

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Edit(int studentID)
        {
            var studentRepository = HttpContext.RequestServices.GetService<IStudentRepository>();
            return View(studentRepository.GetStudentModel(studentID));
        }

        [HttpPost]
        public IActionResult Edit(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.UpdateStudent(student);
                return RedirectToAction("Profile", new { studentID = student.StudentID });
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Create([FromBody]StudentModel student)
        {
            var studentRepository = ActivatorUtilities.CreateInstance<StudentRepository>(HttpContext.RequestServices);
            if (ModelState.IsValid)
            {
                studentRepository.AddStudent(student);
                return RedirectToAction("All");
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int studentID)
        {
            if (studentRepository.DeleteStudent(studentID))
            { 
                return RedirectToAction("All");
            }

            return BadRequest();
        }
    }
}
