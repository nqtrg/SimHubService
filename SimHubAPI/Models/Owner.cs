using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimHubAPI.Models
{
    public class Owner
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public string DateJoined { get; set; }
        public string Sign { get; set; }
        public string Password { get; set; }
    }
}