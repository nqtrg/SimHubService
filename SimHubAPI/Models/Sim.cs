using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimHubAPI.Models
{
    public class Sim
    {

        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        
        public string Price { get; set; }
        public string Made { get; set; }
        //public string ownerName { get; set; }
        public string Type { get; set; }

        // Foreign Key
        public int OwnerId { get; set; }
        // Navigation property
        public Owner Owner { get; set; }
    }

    public class SimDTO
    {

        //public int Id { get; set; }

        public string Number { get; set; }

        public string Price { get; set; }
        public string Made { get; set; }
        //public string ownerName { get; set; }
        public string Type { get; set; }

        // Foreign Key
        public int OwnerId { get; set; }
        // Navigation property
        //public Owner Owner { get; set; }
    }
}