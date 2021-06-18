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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IMapper _mapper;

        public SpecialtiesController(ISpecialtyService specialtyService, IMapper mapper)
        {
            _specialtyService = specialtyService;
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
        public async Task<IEnumerable<SpecialtyResource>> GetAllAsync()
        {
            var specialties = await _specialtyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Specialty>, IEnumerable<SpecialtyResource>>(specialties);
            return resources;
        }


        [SwaggerOperation(
        Summary = "Get Specialty by Id",
        Description = "Get Specialty by Id",
        OperationId = "GerSpecialtyById")]
        [SwaggerResponse(200, "Get Specialty by Id", typeof(SpecialtyResource))]

        [HttpGet("{specialtyId}")]
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int specialtyId)
        {
            var result = await _specialtyService.GetByIdAsync(specialtyId);
            if (!result.Success)
                return BadRequest(result.Message);
            var artistResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
            return Ok(artistResource);
        }
    }
}
