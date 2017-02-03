using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev.Models
{
    public class VirtualClass
    {

        public int VirtualClassID { get; set; }

        [Required(ErrorMessage = "Name Virtual Class is required.")]
        [Display(Name = "Name Virtual Class")]
        public string NameVirtualClass { get; set; }

        [Display(Name = "Training")]
        public int TrainingID { get; set; }

        public virtual Training Training { get; set; }
        public virtual Student Student { get; set; }
        public ICollection<Student> students { get; set; }

        public ICollection<Training> trainings { get; set; }

        public VirtualClass()
        {
            trainings = new HashSet<Training>();
            students = new HashSet<Student>();
        }

    }
}