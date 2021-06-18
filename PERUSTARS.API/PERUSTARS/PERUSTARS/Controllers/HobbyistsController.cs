using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services;
using PERUSTARS.Extensions;
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
    public class HobbyistsController : ControllerBase
    {
        private readonly IHobbyistService _hobbyistService;
        private readonly IMapper _mapper;

        public HobbyistsController(IHobbyistService hobbyistService, IMapper mapper)
        {
            _hobbyistService = hobbyistService;
            _mapper = mapper;
        }



        /*****************************************************************/
                                  /*LIST OF HOBBYISTS*/
        /*****************************************************************/

        [SwaggerOperation(
        Summary = "List all Hobbyists",
        Description = "List of all Hobbyists",
        OperationId = "ListAllHobbyists")]
        [SwaggerResponse(200, "List of Hobbyists", typeof(IEnumerable<HobbyistResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HobbyistResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<HobbyistResource>> GetAllAsync()
        {
            var hobbyists = await _hobbyistService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Hobbyist>, IEnumerable<HobbyistResource>>(hobbyists);
            return resources;
        }



        /*****************************************************************/
                                /*GET HOBBYIST BY ID*/
        /*****************************************************************/

        [SwaggerOperation(
        Summary = "Get Hobbyist by Id",
        Description = "Get Hobbyist by Id",
        OperationId = "GetHobbyistById")]
        [SwaggerResponse(200, "Get Hobbyist by Id", typeof(HobbyistResource))]

        [HttpGet("{hobbyistId}")]
        [ProducesResponseType(typeof(HobbyistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int hobbyistId)
        {
            var result = await _hobbyistService.GetByIdAsync(hobbyistId);
            if (!result.Success)
                return BadRequest(result.Message);
            var hobbyistResource = _mapper.Map<Hobbyist, HobbyistResource>(result.Resource);
            return Ok(hobbyistResource);
        }



        /*****************************************************************/
                                /*SAVE HOBBYIST*/
        /*****************************************************************/

        [SwaggerOperation(
          Summary = "Save Hobbyist",
          Description = "Save Hobbyist",
          OperationId = "SaveHobbyist")]
        [SwaggerResponse(200, "Save hobbyist", typeof(HobbyistResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ArtistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveHobbyistResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var hobbyist = _mapper.Map<SaveHobbyistResource, Hobbyist>(resource);
            var result = await _hobbyistService.SaveAsync(hobbyist);

            if (!result.Success)
                return BadRequest(result.Message);
            var hobbyistResource = _mapper.Map<Hobbyist, HobbyistResource>(result.Resource);
            return Ok(hobbyistResource);
        }



        /*****************************************************************/
                                /*UPDATE HOBBYIST*/
        /*****************************************************************/

        [SwaggerOperation(
        Summary = "Update Hobbyist",
        Description = "Update Hobbyist",
        OperationId = "UpdateHobbyist")]
        [SwaggerResponse(200, "Update Hobbyist", typeof(HobbyistResource))]

        [HttpPut("{hobbyistId}")]
        [ProducesResponseType(typeof(HobbyistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(long hobbyistId, [FromBody] SaveHobbyistResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var hobbyist = _mapper.Map<SaveHobbyistResource, Hobbyist>(resource);
            var result = await _hobbyistService.UpdateAsync(hobbyistId, hobbyist);

            if (!result.Success)
                return BadRequest(result.Message);

            var hobbyistResource = _mapper.Map<Hobbyist, HobbyistResource>(result.Resource);
            return Ok(hobbyistResource);
        }



        /*****************************************************************/
                                /*DELETE HOBBYIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Delete Hobbyist",
           Description = "Delete Hobbyist",
           OperationId = "DeleteHobbyist")]
        [SwaggerResponse(200, "Delete hobbyist", typeof(HobbyistResource))]

        [HttpDelete("{hobbyistId}")]
        [ProducesResponseType(typeof(HobbyistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(long hobbyistId)
        {
            var result = await _hobbyistService.DeleteAsync(hobbyistId);
            if (!result.Success)
                return BadRequest(result.Message);
            var hobbyistResource = _mapper.Map<Hobbyist, HobbyistResource>(result.Resource);
            return Ok(hobbyistResource);
        }
    }
}
