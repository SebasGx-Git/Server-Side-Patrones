using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PERUSTARS.Domain.Models;

namespace PERUSTARS.Resources
{
    public class SaveEventResource
    {
        [Required]
        [MaxLength(100)]
        public string EventTitle { get; set; }


        [Required]
        public ETypeOfEvent EventType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }

        [Required]
        [MaxLength(250)]
        public string EventDescription { get; set; }

        public string EventAditionalInfo { get; set; }

    }
}
