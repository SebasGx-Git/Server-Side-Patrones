using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services.Communications
{
    public class EventAssistanceResponse : BaseResponse<EventAssistance>
    {
        public EventAssistanceResponse(EventAssistance resource) : base(resource)
        {
        }

        public EventAssistanceResponse(string message) : base(message)
        {
        }
    }
}
