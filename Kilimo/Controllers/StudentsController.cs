using Kilimo.DBConnection;
using Kilimo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kilimo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SQLOperations _sqlOperations;

        public StudentsController(SQLOperations sqlOperations)
        {
            _sqlOperations = sqlOperations;
        }
        public IActionResult Students()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                bool success = _sqlOperations.CreateStudent(student); 
                return Json(new { success });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        // Action to get the list of students
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _sqlOperations.GetStudents(); // Implement GetStudents method in SQLOperations
            return PartialView("_StudentListPartial", students);
        }
        [HttpGet]
        public JsonResult GetStudent(int id)
        {
            var student = _sqlOperations.GetStudentById(id);
            if (student != null)
            {
                return Json(new
                {
                    student.StudentName,
                    student.StudentId,
                    ClassStreamName = student.ClassStreamId // Ensure this property exists
                });
            }
            return Json(null);
        }

        // Action to delete a student
        [HttpPost]
        public JsonResult DeleteStudent(int id)
        {
            bool success = _sqlOperations.DeleteStudent(id);
            return Json(new { success });
        }

        // Action to update a student
        [HttpPost]
        public JsonResult UpdateStudent(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    StudentName = studentViewModel.StudentName,
                    StudentId = studentViewModel.StudentId,
                    ClassStreamId = studentViewModel.ClassStreamId
                };
                bool success = _sqlOperations.UpdateStudent(student);
                return Json(new { success });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }
    }
}

