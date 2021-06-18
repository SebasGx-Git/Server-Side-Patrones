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
    [Route("api/hobbyists/{hobbyistId}/claimTickets")]
    [Produces("application/json")]
    [ApiController]
    public class HobbyistClaimTicketsControllers : ControllerBase
    {
        private readonly IClaimTicketService _claimTicketService;
        private readonly IMapper _mapper;


        public HobbyistClaimTicketsControllers(IClaimTicketService claimTicketService, IMapper mapper)
        {
            _claimTicketService = claimTicketService;
            _mapper = mapper;
        }

        /*****************************************************************/
                    /*LIST OF CLAIM TICKETS BY HOBBYIST ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get All Claim Tickets By Hobbyist Id",
           Description = "Get All Claim Tickets By Hobbyist Id",
           OperationId = "GetAllClaimTicketsByHobbyistId")]
        [SwaggerResponse(200, "Get All Claim Tickets By Hobbyist Id", typeof(IEnumerable<HobbyistResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClaimTicketResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<ClaimTicketResource>> GetAllByArtistIdAsync(long hobbyistId)
        {
            var claimTicket = await _claimTicketService.ListAsyncByPersonId(hobbyistId);
            var resources = _mapper.Map<IEnumerable<ClaimTicket>, IEnumerable<ClaimTicketResource>>(claimTicket);
            return resources;
        }



        /*****************************************************************/
                    /*GET CLAIM TICKET OF HOBBYIST BY ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get Claim Ticket Of Hobbyist By Id",
           Description = "Get Claim Ticket Of Hobbyist By Id",
           OperationId = "GetClaimTicketOfHobbyistById")]
        [SwaggerResponse(200, "Get Claim Ticket Of Hobbyist By Id", typeof(ClaimTicketResource))]

        [HttpGet("{claimTicketId}")]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(long hobbyistId, long claimTicketId)
        {
            var result = await _claimTicketService.GetByIdAndPersonIdAsync(hobbyistId, claimTicketId);
            if (!result.Success)
                return BadRequest(result.Message);
            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);
        }



        /*****************************************************************/
                        /*SAVE CLAIM TICKET OF HOBBYIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Save Claim Ticket Of Hobbyist",
           Description = "Save Claim Ticket Of Hobbyist",
           OperationId = "SaveClaimTicketOfHobbyist")]
        [SwaggerResponse(200, "ClaimTicket saved", typeof(ClaimTicketResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(long hobbyistId, [FromBody] SaveClaimTicketResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var claimTicket = _mapper.Map<SaveClaimTicketResource, ClaimTicket>(resource);
            var result = await _claimTicketService.SaveAsync(hobbyistId, claimTicket);

            if (!result.Success)
                return BadRequest(result.Message);
            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);

        }



        /*****************************************************************/
                        /*UPDATE CLAIM TICKET OF HOBBYIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Update Claim Ticket Of Hobbyist",
           Description = "Update Claim Ticket Of Hobbyist",
           OperationId = "UpdateClaimTicketOfHobbyist")]
        [SwaggerResponse(200, "ClaimTicket updated", typeof(ClaimTicketResource))]

        [HttpPut("{claimTicketId}")]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(long hobbyistId, long claimTicketId, [FromBody] SaveClaimTicketResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var claimTicket = _mapper.Map<SaveClaimTicketResource, ClaimTicket>(resource);
            var result = await _claimTicketService.UpdateAsync(hobbyistId, claimTicketId, claimTicket);

            if (!result.Success)
                return BadRequest(result.Message);

            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);
        }


        /*****************************************************************/
                    /*DELETE CLAIM TICKET OF HOBBYIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Delete Claim Ticket Of Hobbyist",
           Description = "Delete Claim Ticket Of Hobbyist",
           OperationId = "DeleteClaimTicketOfHobbyist")]
        [SwaggerResponse(200, "ClaimTicket deleted", typeof(ClaimTicketResource))]

        [HttpDelete("{claimTicketId}")]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(long hobbyistId, long claimTicketId)
        {
            var result = await _claimTicketService.DeleteAsync(hobbyistId, claimTicketId);
            if (!result.Success)
                return BadRequest(result.Message);
            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);
        }
    }
}
