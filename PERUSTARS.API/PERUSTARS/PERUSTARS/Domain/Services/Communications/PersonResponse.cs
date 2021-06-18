using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services.Communications
{
    public class PersonResponse : BaseResponse<Person>
    {
        public PersonResponse(Person resource) : base(resource)
        {
        }

        public PersonResponse(string message) : base(message)
        {
        }
    }
}
