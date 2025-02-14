using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models.DTO;
using AlumniManagement.Models.Repositories;

namespace AlumniManagement.Controllers
{
    public class MajorController : Controller
    {
        private IMajor _majorRepository;
        private IFaculty _facultyRepository;

        public MajorController() : this(
            new Major(),
            new Faculty()
            )
        {
        }
        public MajorController(IMajor majorRepository, IFaculty facultyRepository)
        {
            _majorRepository = majorRepository;
            _facultyRepository = facultyRepository;
        }

        // GET: Major
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMajors()
        {
            var majors = _majorRepository.GetMajors();
            return Json(new { data = majors }, JsonRequestBehavior.AllowGet);
        }

        // GET: Major/Create
        public ActionResult Create()
        {
            var facultyList = _facultyRepository.GetFaculties()
                .Select(f => new SelectListItem
                {
                    Value = f.FacultyID.ToString(),
                    Text = f.FacultyName
                })
                .ToList();


            ViewBag.FacultyLists = new SelectList(facultyList, "Value", "Text");

            return View();
        }

        // POST: Major/Create
        [HttpPost]
        public ActionResult Create(MajorDTO majorDTO)
        {
            try
            {
               if (ModelState.IsValid)
                {
                    _majorRepository.AddMajor(majorDTO);
                    TempData["SuccessMessage"] = "Major added successfully";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }            
        }

        // GET: Major/Edit/5
        public ActionResult Edit(int id)
        {
            var existingMajor = _majorRepository.GetMajorByID(id);

            if (existingMajor == null)
            {
                TempData["ErrorMessage"] = "Major not found";
                return RedirectToAction("Index");
            }

            var facultyList = _facultyRepository.GetFaculties()
                .Select(f => new SelectListItem
                {
                    Value = f.FacultyID.ToString(),
                    Text = f.FacultyName
                }).ToList();

            ViewBag.FacultyLists = new SelectList(facultyList, "Value", "Text", existingMajor.FacultyID);

            return View(existingMajor);
        }

        // POST: Major/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MajorDTO majorDTO)
        {
            try
            {
                // TODO: Add update logic here
                var existingMajor = _majorRepository.GetMajorByID(id);
                if (existingMajor == null)
                {
                    TempData["ErrorMessage"] = "Major not found";
                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    _majorRepository.UpdateMajor(majorDTO);
                    TempData["SuccessMessage"] = "Major updated successfully";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save due to " + ex.Message);
                showFacultyDdl();
                return View();
            }
        }

        // POST: Major/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var existingMajor = _majorRepository.GetMajorByID(id);
                if (existingMajor == null)
                {
                    TempData["ErrorMessage"] = "Major not found";
                    return RedirectToAction("Index");
                }
                _majorRepository.DeleteMajor(id);

                return Json(new { success = true, message = "Major deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Unable to delete due to " + ex.Message });
            }
        }

        private void showFacultyDdl()
        {
            var dropdownList = _facultyRepository.GetFaculties();
            ViewBag.FacultyList = new SelectList(dropdownList, "FacultyID", "FacultyName");
        }
    }
}
