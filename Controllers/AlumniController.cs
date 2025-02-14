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
        private IFaculty _facultyRepository;
        private IMajor _majorRepository;
        private IExcelService _excelService;

        public AlumniController() : this(
            new Alumni(),
            new Faculty(),
            new Major(),
            new ExcelService()
            )
        {
        }
        public AlumniController(IAlumni alumniRepository, IFaculty facultyRepository, IMajor majorRepository ,IExcelService excelService)
        {
            _alumniRepository = alumniRepository;
            _facultyRepository = facultyRepository;
            _majorRepository = majorRepository;
            _excelService = excelService;
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

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
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
