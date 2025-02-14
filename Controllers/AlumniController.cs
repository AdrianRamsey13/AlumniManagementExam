using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models.Repositories;
using AlumniManagement.Models.DTO;

namespace AlumniManagement.Controllers
{
    public class AlumniController : Controller
    {
        private IAlumni _alumniRepository;

        public AlumniController() : this(
            new Alumni()
            )
        {
        }
        public AlumniController(IAlumni alumniRepository)
        {
            _alumniRepository = alumniRepository;
        }

        // GET: Alumni
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAlumnis()
        {
            var alumnis = _alumniRepository.GetAlumnis();
            return Json(new { data = alumnis }, JsonRequestBehavior.AllowGet);
        }

        // GET: Alumni/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alumni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alumni/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumni/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Alumni/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumni/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alumni/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
