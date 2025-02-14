using AlumniManagement.AlumniService;
using AlumniManagement.FacultyService;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.JobHistoryService;

namespace AlumniManagement.Models.Repositories
{
    public class JobHistory : IJobHistory
    {
        private JobHistoryServicesClient _jobHistoryServiceClient;

        public JobHistory()
        {
            _jobHistoryServiceClient = new JobHistoryServicesClient();
        }
        public IEnumerable<JobHistoryDTO> GetJobHistories(int alumniID)
        {
            var data = _jobHistoryServiceClient.GetJobHistories(alumniID);
            var result = data.Select(a => Mapping.Mapper.Map<JobHistoryDTO>(a)).ToList();
            return result;
        }

        public JobHistoryDTO GetJobHistoryByID(int alumniID, int jobHistoryID)
        {
            var data = _jobHistoryServiceClient.GetJobHistoryByID(alumniID, jobHistoryID);
            var result = Mapping.Mapper.Map<JobHistoryDTO>(data);
            return result;
        }

        public void AddJobHistory(DTO.JobHistoryDTO jobHistory, int alumniID)
        {
            var data = Mapping.Mapper.Map<JobHistoryService.JobHistoryDTO>(jobHistory);
            _jobHistoryServiceClient.AddJobHistory(data, alumniID);
        }

        public void DeleteJobHistory(int alumniID, int jobHistoryID)
        {
            _jobHistoryServiceClient.DeleteJobHistory(alumniID, jobHistoryID);
        }

        public void UpdateJobHistory(DTO.JobHistoryDTO jobHistory, int alumniID)
        {
            var result = Mapping.Mapper.Map<JobHistoryService.JobHistoryDTO>(jobHistory);
            _jobHistoryServiceClient.UpdateJobHistory(result, alumniID);
        }
    }
}