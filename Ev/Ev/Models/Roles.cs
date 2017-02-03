using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set; }

        public ICollection<UserAccount> userAccount { get; set; }

    }
}