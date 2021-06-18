using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services;
using PERUSTARS.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PERUSTARS.Controllers
{
    [Route("/api/hobbyists/{hobbyistId}/follows")]
    [Produces("application/json")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public FollowsController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }



        /*****************************************************************/
                       /*LIST OF ALL ARTISTS BY HOBBYIST ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get All Artists By Hobbyist Id",
           Description = "Get All Artists By Hobbyist Id",
           OperationId = "GetAllByHobbyistId")]
        [SwaggerResponse(200, "Get All By HobbyistId", typeof(IEnumerable<ArtistResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtistResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<ArtistResource>> GetAllByHobbyistIdAsync(int hobbyistId)
        {
            var artists = await _artistService.ListByHobbyistIdAsync(hobbyistId);
            var resources = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistResource>>(artists);
            return resources;
        }    
    }
}
