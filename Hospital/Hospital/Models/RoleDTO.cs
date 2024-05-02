using Microsoft.AspNetCore.Identity;

namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class RoleDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        //public ICollection<User> Users { get; set; }

    }
}
