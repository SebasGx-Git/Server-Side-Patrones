using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PERUSTARS.Domain.Services.Communications
{
    public class HobbyistResponse : BaseResponse<Hobbyist>
    {
        public HobbyistResponse(Hobbyist resource) : base(resource)
        {
        }

        public HobbyistResponse(string message) : base(message)
        {
        }
    }
}
