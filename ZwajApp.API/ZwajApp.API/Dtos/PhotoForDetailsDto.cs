using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.API.Dtos
{
    public class PhotoForDetailsDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }
      

    }
}
