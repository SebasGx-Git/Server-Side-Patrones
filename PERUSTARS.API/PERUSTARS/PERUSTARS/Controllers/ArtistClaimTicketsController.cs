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
    [Route("api/artists/{artistId}/claimTickets")]
    [Produces("application/json")]
    [ApiController]
    public class ArtistClaimTicketsController : ControllerBase
    {
        private readonly IClaimTicketService _claimTicketService;
        private readonly IMapper _mapper;

        public ArtistClaimTicketsController(IClaimTicketService claimTicketService, IMapper mapper)
        {
            _claimTicketService = claimTicketService;
            _mapper = mapper;
        }
        


        /*****************************************************************/
                        /*LIST OF CLAIM TICKETS BY ARTIST ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get All Claim Tickets By Artist Id",
           Description = "Get All Claim Tickets By Artist Id",
           OperationId = "GetAllClaimTicketsByArtistId")]
        [SwaggerResponse(200, "Get All Claim Tickets By Artist Id", typeof(IEnumerable<HobbyistResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClaimTicketResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<ClaimTicketResource>> GetAllByArtistIdAsync(long artistId)
        {
            var claimTicket = await _claimTicketService.ListAsyncByPersonId(artistId);
            var resources = _mapper.Map<IEnumerable<ClaimTicket>, IEnumerable<ClaimTicketResource>>(claimTicket);
            return resources;
        }



        /*****************************************************************/
                        /*GET CLAIM TICKET OF ARTIST BY ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get Claim Ticket Of Artist By Id",
           Description = "Get Claim Ticket Of Artist By Id",
           OperationId = "GetClaimTicketOfArtistById")]
        [SwaggerResponse(200, "Get Claim Ticket Of Artist By Id", typeof(ClaimTicketResource))]

        [HttpGet("{claimTicketId}")]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(long artistId, long claimTicketId)
        {
            var result = await _claimTicketService.GetByIdAndPersonIdAsync(artistId, claimTicketId);
            if (!result.Success)
                return BadRequest(result.Message);
            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);
        }



        /*****************************************************************/
                            /*SAVE CLAIM TICKET OF ARTIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Save Claim Ticket Of Artist",
           Description = "Save Claim Ticket Of Artist",
           OperationId = "SaveClaimTicketOfArtist")]
        [SwaggerResponse(200, "ClaimTicket saved", typeof(ClaimTicketResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(long artistId, [FromBody] SaveClaimTicketResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var claimTicket = _mapper.Map<SaveClaimTicketResource, ClaimTicket>(resource);
            var result = await _claimTicketService.SaveAsync(artistId, claimTicket);

            if (!result.Success)
                return BadRequest(result.Message);
            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);

        }



        /*****************************************************************/
                           /*UPDATE CLAIM TICKET OF ARTIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Update Claim Ticket Of Artist",
           Description = "Update Claim Ticket Of Artist",
           OperationId = "UpdateClaimTicketOfArtist")]
        [SwaggerResponse(200, "ClaimTicket updated", typeof(ClaimTicketResource))]

        [HttpPut("{claimTicketId}")]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(long artistId, long claimTicketId, [FromBody] SaveClaimTicketResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var claimTicket = _mapper.Map<SaveClaimTicketResource, ClaimTicket>(resource);
            var result = await _claimTicketService.UpdateAsync(artistId, claimTicketId, claimTicket);

            if (!result.Success)
                return BadRequest(result.Message);

            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);
        }


        /*****************************************************************/
                            /*DELETE CLAIM TICKET OF ARTIST*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Delete Claim Ticket Of Artist",
           Description = "Delete Claim Ticket Of Artist",
           OperationId = "DeleteClaimTicketOfArtist")]
        [SwaggerResponse(200, "ClaimTicket deleted", typeof(ClaimTicketResource))]

        [HttpDelete("{claimTicketId}")]
        [ProducesResponseType(typeof(ClaimTicketResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(long artistId, long claimTicketId)
        {
            var result = await _claimTicketService.DeleteAsync(artistId, claimTicketId);
            if (!result.Success)
                return BadRequest(result.Message);
            var claimTicketResource = _mapper.Map<ClaimTicket, ClaimTicketResource>(result.Resource);
            return Ok(claimTicketResource);
        }

    }
}
