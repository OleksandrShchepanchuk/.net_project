using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    public class UserDTO
    {

        public int UserId { get; set; }

        // [JsonPropertyName("userName")] 
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public ICollection<Role> Roles { get; set; }
        //public ICollection<Wish> Wishes { get; set; }
        //public ICollection<Review> Reviews { get; set; }
    }
}
