using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services;
using PERUSTARS.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Controllers
{
    [ApiController]
    [Route("api/hobbyists/{hobbyistId}/specialties")]
    [Produces("application/json")]
    public class InterestsController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IInterestService _interestService;
        private readonly IMapper _mapper;

        public InterestsController(ISpecialtyService specialtyService, IInterestService interestService, IMapper mapper)
        {
            _specialtyService = specialtyService;
            _interestService = interestService;
            _mapper = mapper;
        }

        [SwaggerOperation(
        Summary = "List all Specialties",
        Description = "List of all Specialties",
        OperationId = "ListAllSpecialties")]
        [SwaggerResponse(200, "List of Specialties", typeof(IEnumerable<SpecialtyResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpecialtyResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SpecialtyResource>> GetAllByHobbyistIdAsync(int hobbyistId)
        {
            var specialties = await _specialtyService.ListByHobbyistIdAsync(hobbyistId);
            var resources = _mapper.Map<IEnumerable<Specialty>, IEnumerable<SpecialtyResource>>(specialties);
            return resources;
        }

        [SwaggerOperation(
        Summary = "Assign Interest",
        Description = "Assign Interest",
        OperationId = "AssignInterest")]
        [SwaggerResponse(200, "Assign Interest", typeof(SpecialtyResource))]

        [HttpPost("{specialtyId}")]
        [ProducesResponseType(typeof(ArtistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignHobbyistSpecialty(int hobbyistId, int specialtyId)
        {
            var result = await _interestService.AssingInterestAsync(hobbyistId, specialtyId);          

            if (!result.Success)
                return BadRequest(result.Message);

            var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource.Specialty);
            return Ok(specialtyResource);

        }


        [SwaggerOperation(
        Summary = "Unassign Interest",
        Description = "Unassign Interest",
        OperationId = "UnassignInterest")]
        [SwaggerResponse(200, "Unassign Interest", typeof(SpecialtyResource))]

        [HttpDelete("{specialtyId}")]
        [ProducesResponseType(typeof(ArtistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignHobbyistSpecialty(int hobbyistId, int specialtyId)
        {
            var result = await _interestService.UnassignInterestAsync(hobbyistId, specialtyId);

            if (!result.Success)
                return BadRequest(result.Message);

            var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource.Specialty);
            return Ok(specialtyResource);

        }

    }
}
