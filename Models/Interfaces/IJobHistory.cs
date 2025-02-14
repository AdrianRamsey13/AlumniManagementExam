using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.JobHistoryService;
using AlumniManagement.Models;
using System.ServiceModel;

namespace AlumniManagement.Models.Interfaces
{
    public interface IJobHistory
    {
        IEnumerable<JobHistoryDTO> GetJobHistories(int alumniID);

        JobHistoryDTO GetJobHistoryByID(int alumniID, int jobHistoryID);

        void AddJobHistory(DTO.JobHistoryDTO jobHistory, int alumniID);

        void UpdateJobHistory(DTO.JobHistoryDTO jobHistory, int alumniID);

        void DeleteJobHistory(int alumniID, int jobHistoryID);
    }
}
