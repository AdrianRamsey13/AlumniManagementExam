using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models.DTO;
using AlumniManagement.Models.Repositories;
using System.ServiceModel.Channels;
using WebGrease.Css.Ast.Selectors;

namespace AlumniManagement.Controllers
{
    public class FacultyController : Controller
    {
        //---------------------------------------------------------------------------------
        private IFaculty _facultyRepository;

        public FacultyController() : this(new Faculty())
        {
        }
        public FacultyController(IFaculty facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        //---------------------------------------------------------------------------------

        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFaculties()
        {
            var faculties = _facultyRepository.GetFaculties();
            return Json(new { data = faculties }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFaculty(int facultyID)
        {
            var faculty = _facultyRepository.GetFacultyByID(facultyID);

            if (faculty == null)
            {
                return Json(new { success = false, errorMsg = "Faculty not found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(faculty, JsonRequestBehavior.AllowGet);
        }

        // GET: Faculty/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculty/Create
        [HttpPost]
        public ActionResult Create(FacultyDTO facultyDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _facultyRepository.AddFaculty(facultyDTO);
                    TempData["SuccessMessage"] = "Faculty added successfully";
                }

                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save due to " + ex.Message);
                return Json(new
                {
                    error = true,
                    errorMsg = "Error creating Faculty" + ex.Message
                });
            }
        }

        // GET: Faculty/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var existingFaculty = _facultyRepository.GetFacultyByID(id);

        //    if (existingFaculty == null)
        //    {
        //        TempData["ErrorMessage"] = "Faculty not found";
        //        return RedirectToAction("Index");
        //    }

        //    return View(existingFaculty);
        //}

        // POST: Faculty/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FacultyDTO facultyDTO)
        {
            try
            {
                // TODO: Add update logic here
                var existingFaculty = _facultyRepository.GetFacultyByID(id);
                if (existingFaculty == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    _facultyRepository.UpdateFaculty(facultyDTO);
                    TempData["SuccessMessage"] = "Faculty updated successfully";
                }

                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save due to " + ex.Message);
                return Json(new
                {
                    error = true,
                    errorMsg = "Error updating Faculty" + ex.Message
                });
            }
        }

        // POST: Faculty/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var existingFaculty = _facultyRepository.GetFacultyByID(id);
                if (existingFaculty == null)
                {
                    return HttpNotFound();
                }

                _facultyRepository.DeleteFaculty(id);
                return Json(new
                {
                    success = true,
                    message = "Faculty Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete" + ex.Message);
                return Json(new
                {
                    success = false,
                    Message = "Unable to delete due to " + ex.Message
                });
            }
        }

        public ActionResult DeleteSelected(int[] ids)
        {
            try
            {
                if (ids != null && ids.Length > 0)
                {
                    foreach (var facultyId in ids)
                    {
                        _facultyRepository.DeleteFaculty(facultyId);
                    }
                    return Json(new
                    {
                        success = true,
                        message = "Faculty Deleted Successfully"
                    });
                }
                return Json(new
                {
                    success = false,
                    message = "Please select a faculty to delete"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to delete due to " + ex.Message
                });
            }
        }
    }
}
