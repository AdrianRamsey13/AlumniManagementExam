using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models.Repositories;
using AlumniManagement.Models.DTO;
using AlumniManagement.Models;

namespace AlumniManagement.Controllers
{
    public class AlumniController : Controller
    {
        private IAlumni _alumniRepository;
        private IFaculty _facultyRepository;
        private IMajor _majorRepository;
        private IExcelService _excelService;
        private IState _stateRepository;
        private IDistrict _districtRepository;

        public AlumniController() : this(
            new Models.Repositories.Alumni(),
            new Models.Repositories.Faculty(),
            new Models.Repositories.Major(),
            new ExcelService(),
            new Models.Repositories.State(),
            new Models.Repositories.District()
            )
        {
        }
        public AlumniController(IAlumni alumniRepository, IFaculty facultyRepository, IMajor majorRepository ,IExcelService excelService, IState stateRepository, IDistrict districtRepository)
        {
            _alumniRepository = alumniRepository;
            _facultyRepository = facultyRepository;
            _majorRepository = majorRepository;
            _excelService = excelService;
            _stateRepository = stateRepository;
            _districtRepository = districtRepository;
        }

        // GET: Alumni
        public ActionResult Index()
        {
            var AlumniDTO = new AlumniDTO()
            {
                States = _stateRepository.GetStates()
                        .Select(s => new SelectListItem
                        {
                            Value = s.StateID.ToString(),
                            Text = s.StateName
                        }).ToList(),
                Faculties = _facultyRepository.GetFaculties()
                           .Select(f => new SelectListItem
                           {
                               Value = f.FacultyID.ToString(),
                               Text = f.FacultyName
                           }),
                Degrees = new List<SelectListItem>
                {
                    new SelectListItem {Value = "S1", Text="S1"},
                    new SelectListItem {Value = "S2", Text="S2"},
                    new SelectListItem {Value = "S3", Text="S3"},
                    new SelectListItem {Value = "S4", Text="S4"},
                }
            };

            return View(AlumniDTO);
        }

        public JsonResult GetAlumnis()
        {
            var alumnis = _alumniRepository.GetAlumnis();
            return Json(new { data = alumnis }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAlumniByID(int alumniID)
        {
            var alumni = _alumniRepository.GetAlumniByID(alumniID);
            var result = Mapping.Mapper.Map<AlumniDTO>(alumni);
            if (alumni == null)
            {
                return Json(new { success = false, errorMsg = "Alumni not found." }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.FacultyLists = _facultyRepository.GetFaculties()
                .Select(f => new SelectListItem
                {
                    Value = f.FacultyID.ToString(),
                    Text = f.FacultyName,
                    Selected = f.FacultyID == alumni.FacultyID
                })
                .ToList();

            ViewBag.StateLists = _stateRepository.GetStates()
                .Select(s => new SelectListItem
                {
                    Value = s.StateID.ToString(),
                    Text = s.StateName,
                    Selected = s.StateID == alumni.StateID
                })
                .ToList();
            ViewBag.DistrictLists = _districtRepository.GetDistrict(alumni.StateID)
                .Select(d => new SelectListItem
                {
                    Value = d.DistrictID.ToString(),
                    Text = d.DistrictName,
                    Selected = d.DistrictID == alumni.DistrictID
                })
                .ToList();
            ViewBag.MajorLists = _majorRepository.GetMajors()
                .Select(m => new SelectListItem
                {
                    Value = m.MajorID.ToString(),
                    Text = m.MajorName,
                    Selected = m.MajorID == alumni.MajorID
                })
                .ToList();
            
            ViewBag.SelectedDegree = alumni.Degree;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Alumni/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alumni/Create
        public ActionResult Create()
        {
            var AlumniDTO = new AlumniDTO()
            {
                States = _stateRepository.GetStates()
                         .Select(s => new SelectListItem
                         {
                             Value = s.StateID.ToString(),
                             Text = s.StateName
                         }).ToList(),
                Faculties = _facultyRepository.GetFaculties()
                            .Select(f => new SelectListItem
                            {
                                Value = f.FacultyID.ToString(),
                                Text = f.FacultyName
                            }),
                Degrees = new List<SelectListItem>
                {
                    new SelectListItem {Value = "S1", Text="S1"},
                    new SelectListItem {Value = "S2", Text="S2"},
                    new SelectListItem {Value = "S3", Text="S3"},
                    new SelectListItem {Value = "S4", Text="S4"},
                }
            };

            return View(AlumniDTO);
        }

        public JsonResult GetDistrict(int stateID)
        {
            var list = _districtRepository.GetDistrict(stateID).Select(d => new AlumniDTO
            {
                DistrictID = d.DistrictID,
                DistrictName = d.DistrictName
            }).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetMajor(int facultyID)
        {
            var list = _majorRepository.GetMajors().Where(f => f.FacultyID == facultyID);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        // POST: Alumni/Create
        [HttpPost]
        public ActionResult Create(AlumniDTO alumniDTO)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _alumniRepository.AddAlumni(alumniDTO);
                    TempData["SuccessMessage"] = "Alumni added successfully";
                }

                return Json(new
                {
                    success = true
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                });
            }
        }

        // GET: Alumni/Edit/5
        public ActionResult Edit(int id)
        {
            var existingAlumni = _alumniRepository.GetAlumniByID(id);
            if (existingAlumni == null)
            {
                TempData["ErrorMessage"] = "Alumni not found";
                return RedirectToAction("Index");
            }

            var facultyList = _facultyRepository.GetFaculties()
                .Select(f => new SelectListItem
                {
                    Value = f.FacultyID.ToString(),
                    Text = f.FacultyName
                })
                .ToList();
            ViewBag.FacultyLists = new SelectList(facultyList, "Value", "Text");

            var majorList = _majorRepository.GetMajors()
                .Select(m => new SelectListItem
                {
                    Value = m.MajorID.ToString(),
                    Text = m.MajorName
                })
                .ToList();

            ViewBag.MajorLists = new SelectList(majorList, "Value", "Text");
            return View();
        }

        // POST: Alumni/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AlumniDTO alumniDTO)
        {
            try
            {
                // TODO: Add update logic here
                var existingAlumni = _alumniRepository.GetAlumniByID(id);
                if (existingAlumni == null)
                {
                    TempData["ErrorMessage"] = "Alumni not found";
                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    _alumniRepository.UpdateAlumni(alumniDTO);
                    TempData["SuccessMessage"] = "Alumni updated successfully";
                }
                return Json(new {success = true, message = "Alumni updated successfully" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save due to " + ex.Message);
                return Json(new {success = false, message = "Unable to update due to " + ex.Message });
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
                var existingAlumni = _alumniRepository.GetAlumniByID(id);
                if (existingAlumni == null)
                {
                    return HttpNotFound();
                }

                _alumniRepository.DeleteAlumni(id);
                return Json(new
                {
                    success = true,
                    message = "Alumni deleted successfully"
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete due to " + ex.Message);
                return Json(new
                {
                    success = false,
                    message = "Unable to delete due to " + ex.Message
                });
            }
        }

        public ActionResult DeleteSelected(int[] ids)
        {
            try
            {
                if (ids != null && ids.Length > 0)
                {
                    foreach (var alumniId in ids)
                    {
                        _alumniRepository.DeleteAlumni(alumniId);
                    }
                    return Json(new
                    {
                        success = true,
                        message = "Alumni Deleted Successfully"
                    });
                }
                return Json(new
                {
                    success = false,
                    message = "Please Select a alumni to delete"
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to delete due to " + ex.Message
                });
            }
        }

        //-------------------------------------------------Import Export Excel-------------------------------------------------

        public ActionResult AlumniExport()
        {
            var workbook = _excelService.AlumniExportToExcel();

            //save workbook
            var stream = new System.IO.MemoryStream();  

            workbook.Save(stream, Aspose.Cells.SaveFormat.Xlsx);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Alumni.xlsx");
        }
    }
}
