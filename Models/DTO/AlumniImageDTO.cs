using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlumniManagement.Models.DTO
{
    public class AlumniImageDTO
    {
        [Key]
        public int ImageID { get; set; }

        public int AlumniID { get; set; }

        [Required]
        [DisplayName("File")]
        public string FileName { get; set; }

        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Upload Date")]
        public System.DateTime UploadDate { get; set; }
    }
}