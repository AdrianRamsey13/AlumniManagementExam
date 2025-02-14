using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.Configuration.Annotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AlumniManagement.Models.DTO
{
    public class JobHistoryDTO
    {
        [Key]
        public int JobHistoryID { get; set; }

        public int AlumniID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        [StringLength(50)]
        [Required]
        [DisplayName("Company")]
        public string Company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-MMM-yyyy hh:mm:ss tt")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-MMM-yyyy hh:mm:ss tt")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [StringLength(100)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-MMM-yyyy hh:mm:ss tt")]
        [DisplayName("Modified Date")]
        public System.DateTime ModifiedDate { get; set; }

        public string Alumni { get; set; }
    }
}