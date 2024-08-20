using Microsoft.AspNetCore.Mvc;
using Kilimo.Models;
using Kilimo.DBConnection;
using System.Collections.Generic;
using System.Linq;

namespace Kilimo.Controllers
{
    public class StreamsController : Controller
    {
        private readonly SQLOperations _sqlOperations;

        public StreamsController(SQLOperations sqlOperations)
        {
            _sqlOperations = sqlOperations;
        }

        // Action to display the form
        public IActionResult Streams()
        {
            return View(); 
        }
        public IActionResult GetClasses()
        {
            return View();
        }

        // Get class streams for displaying in a partial view
        [HttpGet]
        public IActionResult GetClassStreams()
        {
            var streams = _sqlOperations.GetClassStreams();
            return PartialView("_StreamsList", streams); // Ensure "_StreamsList.cshtml" is in Views/Streams
        }

        [HttpPost]
        public IActionResult CreateClassStream(ClassStream classStream)
        {
            if (ModelState.IsValid)
            {
                bool success = _sqlOperations.CreateClassStream(classStream);
                return Json(new { success });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }

        // Get a specific class stream by ID
        [HttpGet]
        public IActionResult GetClassStream(int id)
        {
            var classStream = _sqlOperations.GetClassStreamById(id);
            if (classStream != null)
            {
                return Json(classStream);
            }
            return Json(new { success = false, message = "Class stream not found." });
        }

        // Get students by class stream ID
        [HttpGet]
        public IActionResult GetStudentsByClassStream(int classStreamId)
        {
            var students = _sqlOperations.GetStudentsByClassStream(classStreamId);
            return Json(students);
        }
    }
}
