using Fochso.Models.Class;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fochso.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassController : Controller
    {

        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        // GET: StudentController
        [AllowAnonymous]
        public ActionResult Index()
        {
            var response = _classService.GetAllClass();
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            return View(response.Data);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var response = _classService.GetClass(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Class", "Index");
            }
            return View(response.Data);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        public ActionResult Create(CreateClassViewModel createClass)
        {
            var response = _classService.CreateClass(createClass);
            if (response.Status is false)
            {
                return View(createClass);
            }

            return RedirectToAction("Class", "Index");
        }

        // GET: StudentController/Edit/5
        public ActionResult Update(int id)
        {
            var response = _classService.GetClass(id);
            if (response.Status is false)
            {
                return RedirectToAction("Class", "Index");
            }
            return View(response);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        public ActionResult Update(int id, UpdateClassViewModel updateClass)
        {
            var response = _classService.UpdateClass(id, updateClass);

            if (response.Status is false)
            {
                return View(response);
            }

            return RedirectToAction("Class", "Index");
        }

        // GET: StudentController/Delete/5

        // POST: StudentController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromRoute] int id)
        {
            var response = _classService.DeleteClass(id);
            if (response.Status is false)
            {
                return View(response);
            }
            return RedirectToAction("Class", "Index");
        }
    }
}
