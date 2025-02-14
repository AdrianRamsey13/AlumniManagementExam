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
    public class JobHistoryController : Controller
    {
        private IJobHistory _jobHistoryRepository;

        public JobHistoryController() : this(new JobHistory())
        {
        }
        public JobHistoryController(IJobHistory jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        // GET: JobHistory
        public ActionResult Index(int alumniID)
        {
            var jobHistories = _jobHistoryRepository.GetJobHistories(alumniID);
            return View(jobHistories);
        }

        public JsonResult GetJobHistories(int alumniID)
        {
            var jobHistories = _jobHistoryRepository.GetJobHistories(alumniID);
            return Json(new { data = jobHistories }, JsonRequestBehavior.AllowGet);
        }

        // GET: JobHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobHistory/Create
        [HttpPost]
        public ActionResult Create(int alumniID ,JobHistoryDTO jobHistoryDTO)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _jobHistoryRepository.AddJobHistory(jobHistoryDTO, alumniID);
                    TempData["SuccessMessage"] = "Job History added successfully";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to add Job History due to " + ex.Message;
                return View();
            }
        }

        // GET: JobHistory/Edit/5
        public ActionResult Edit(int id, int alumniID)
        {
            return View();
        }

        // POST: JobHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, JobHistoryDTO jobHistoryDTO, int alumniID)
        {
            try
            {
                // TODO: Add update logic here
                var jobHistory = _jobHistoryRepository.GetJobHistoryByID(alumniID, id);
                if (jobHistory == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    _jobHistoryRepository.UpdateJobHistory(jobHistoryDTO, alumniID);
                    TempData["SuccessMessage"] = "Job History updated successfully";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to update due to " + ex.Message);
                return View();
            }
        }

        // POST: JobHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection, int alumniID)
        {
            try
            {
                // TODO: Add delete logic here
                var existingJobHistory = _jobHistoryRepository.GetJobHistoryByID(alumniID, id);
                if (existingJobHistory == null)
                {
                    TempData["ErrorMessage"] = "Job History not found";
                    return RedirectToAction("Index");
                }
                _jobHistoryRepository.DeleteJobHistory(alumniID, id);
                return Json(new { success = true, message = "Job History deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete due to " + ex.Message);
                return Json(new { success = false, message = "Failed to delete Job History" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
