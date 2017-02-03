using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev.Models
{
    public class Training
    {
        public int TrainingID { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Name Training is required.")]
        [Display(Name = "Name Training")]
        public string NameTraining { get; set; }

        [Required(ErrorMessage = "Data Start is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Start")]
        public DateTime DateStartOfTraining { get; set; }

        public ICollection<VirtualClass> virtualClasses { get; set; }

        public Training()
        {
            virtualClasses = new HashSet<VirtualClass>();
        }
    }
}