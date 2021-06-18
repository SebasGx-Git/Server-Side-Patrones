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
    [Route("/api/artists/{artistId}/followers")]
    [Produces("application/json")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowerService _followerService;
        private readonly IHobbyistService _hobbyistService;
        private readonly IMapper _mapper;

        public FollowersController(IFollowerService followerService, IMapper mapper, IHobbyistService hobbyistService)
        {
            _followerService = followerService;
            _mapper = mapper;
            _hobbyistService = hobbyistService;
        }



        /*****************************************************************/
                                 /*ASSIGN FOLLOWER*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Assign Follower",
           Description = "Assign Follower",
           OperationId = "AssignFollower")]
        [SwaggerResponse(200, "Artist Assign Follower", typeof(ArtistResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ArtistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignFollower(int hobbyistId, int artistId)
        {
            var result = await _followerService.AssignFollowerAsync(hobbyistId, artistId);
            if (!result.Success)
                return BadRequest(result.Message);

            var artistResource = _mapper.Map<Artist, ArtistResource>(result.Resource.Artist);
            return Ok(artistResource);
        }



        /*****************************************************************/
                                /*UNASSIGN FOLLOWER*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Unassign Follower",
           Description = "Unassign Follower",
           OperationId = "UnassignFollower")]
        [SwaggerResponse(200, "Artist Unassign Follower", typeof(ArtistResource))]

        [HttpDelete("{hobbyistId}")]
        [ProducesResponseType(typeof(ArtistResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignFollower(int hobbyistId, int artistId)
        {
            var result = await _followerService.UnassignFollowerAsync(hobbyistId, artistId);
            if (!result.Success)
                return BadRequest(result.Message);

            var artistResource = _mapper.Map<Artist, ArtistResource>(result.Resource.Artist);
            return Ok(artistResource);
        }



        /*****************************************************************/
                    /*LIST OF ALL HOBBYISTS BY ARTIST ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get All Hobbyist By Artist Id",
           Description = "Get All Hobbyists By Artist Id",
           OperationId = "GetAllHobbyistsByArtistId")]
        [SwaggerResponse(200, "Get All Hobbyists By Artist Id", typeof(IEnumerable<HobbyistResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HobbyistResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<HobbyistResource>> GetAllByArtistIdAsync(int artistId)
        {
            var hobbyists = await _hobbyistService.ListByArtistIdAsync(artistId);
            var resources = _mapper.Map<IEnumerable<Hobbyist>, IEnumerable<HobbyistResource>>(hobbyists);
            return resources;
        }



        /*****************************************************************/
                          /*COUNT OF ARTISTS' FOLLOWERS*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Count Artists' Followers",
           Description = "Count Artists' Followers",
           OperationId = "CountFollowers")]
        [SwaggerResponse(200, "Count Followers", typeof(int))]

        [HttpGet("count")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<int> CountFollowers(long artistId)
        {
            var count = await _followerService.CountFollowers(artistId);
            return count;
        }
    }
}
