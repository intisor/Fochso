using Fochso.Entities;
using Fochso.Models.Student;
using Fochso.Service.Implementation;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Fochso.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var response = _studentService.GetAllStudent();
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            return View(response.Data);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var response = _studentService.GetStudent(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Student", "Index");
            }
            return View(response.Data);
        }

        // GET: StudentController/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        public ActionResult Create(CreateStudentViewModel createStudent)
        {
                var response = _studentService.CreateStudent(createStudent);
            if(response.Status is false)
            {
                return View(createStudent);
            }

                return RedirectToAction("Student","Index");
        }

        // GET: StudentController/Edit/5
        public ActionResult Update(int id)
        {
            var response = _studentService.GetStudent(id);
            if(response.Status is false) 
            {
                return RedirectToAction("Student", "Index");
            }
            return View(response);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, UpdateStudentViewModel updateStudent)
        {
            var response = _studentService.UpdateStudent(id, updateStudent);

            if(response.Status is false)
            {
                return View(response);
            }

                return RedirectToAction("Student","Index");
            
           
        }

        // GET: StudentController/Delete/5
      
        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute]int id)
        {
            var response = _studentService.DeleteStudent(id);
            if(response.Status is false)
            {
                return View(response);
            }
                return RedirectToAction("Student", "Index");
        }
    }
}
