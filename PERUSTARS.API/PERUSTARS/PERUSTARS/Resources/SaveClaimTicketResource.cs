using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PERUSTARS.Resources
{
    public class SaveClaimTicketResource
    {
        [Required]
        [MaxLength(40)]
        public string ClaimSubject { get; set; }

        [Required]
        [MaxLength(300)]
        public string ClaimDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime IncedentDate { get; set; }

        [Required]
        public long ReportedPersonId { get; set; }


    }
}
