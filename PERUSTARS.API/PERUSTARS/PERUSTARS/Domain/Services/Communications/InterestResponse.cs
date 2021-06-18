using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services.Communications
{
    public class InterestResponse : BaseResponse<Interest>
    {
        public InterestResponse(Interest resource) : base(resource)
        {
        }

        public InterestResponse(string message) : base(message)
        {
        }
    }
}
