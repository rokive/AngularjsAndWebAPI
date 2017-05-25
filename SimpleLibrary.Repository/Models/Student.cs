using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student:Base
    {
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        //[Display(Name = "Student Poto")]
        //public String StudentPoto { get; set; }

        [Required]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string StudentGender { get; set; }

        [Required]
        public string Religion { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DOF { get; set; }

        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Taken Book")]
        public int TakenBook { get; set; }

        //[Display(Name = "Phone")]
        //public string Phone { get; set; }

        //[Required]
        //[Display(Name = "Present Address")]
        //public string PresentAddress { get; set; }

        //[Display(Name = "Permanent Address")]
        //public string PermanentAddress { get; set; }

        //[Required]
        //[Display(Name = "Admission Class")]
        //public int AdmissionClassId { get; set; }

        //[Required]
        //[Display(Name = "Current Class")]
        //public int ClassTableId { get; set; }

        //public string Section { get; set; }

        //public string Group { get; set; }

        //public int Roll { get; set; }        
    }
}
