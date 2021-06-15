using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Resources
{
    public class ClaimTicketResource
    {
        public long ClaimId { get; set; }
        public string ClaimSubject { get; set; }
        public string ClaimDescription { get; set; }
        public DateTime IncedentDate { get; set; }
        public long ReportedPersonId { get; set; }
        public long ReportMadeById { get; set; }
    }
}
