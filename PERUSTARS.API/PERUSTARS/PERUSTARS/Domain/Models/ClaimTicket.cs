using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Models
{
    public class ClaimTicket
    {
        public long ClaimId { get; set; }
        public string ClaimSubject { get; set; }
        public string ClaimDescription { get; set; }
        public DateTime IncedentDate { get; set; }
        public long ReportedPersonId { get; set; }
        public Person ReportedPerson { get; set; }

        //Una persona puede realizar multiples reportes
        public long ReportMadeById { get; set; }
        public Person ReportMadeBy { get; set; }
    }
}
