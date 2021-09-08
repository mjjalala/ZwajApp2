using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZwajApp.API.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string username { get; set; }
        [StringLength(8, MinimumLength = 4, ErrorMessage = "يجب ان لا تزيد كلمة السر عن أربعة أحرف ولا تقل عن ثمانية")]
        public string password { get; set; }
    }
}
