using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZwajApp.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] passwordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        //public string gender { get; set; }
        //public DateTime DateofBirth { get; set; }
        //public string KnownAs { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime LastActive { get; set; }

        //public string Introduction { get; set; }
        //public string LookingFor { get; set; }
        //public string Interests { get; set; }
        //public string City { get; set; }
        //public string Country { get; set; }
        //public ICollection<Photo> Photos { get; set; }
    }
}
