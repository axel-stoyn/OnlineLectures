using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int VirtualClassID { get; set; }
       // public virtual VirtualClass VirtualClass { get; set; }

        public ICollection<VirtualClass> virtualClass { get; set; }
    }
}