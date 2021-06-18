using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services.Communications
{
    public class FollowerResponse : BaseResponse<Follower>
    {
        public FollowerResponse(Follower resource) : base(resource)
        {
        }

        public FollowerResponse(string message) : base(message)
        {
        }
    }
}
