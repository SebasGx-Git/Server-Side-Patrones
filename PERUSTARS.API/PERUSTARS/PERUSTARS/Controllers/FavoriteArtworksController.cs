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
    [Route("api/hobbyists/{hobbyistId}/artworks")]
    [Produces("application/json")]
    [ApiController]
    public class FavoriteArtworksController : ControllerBase
    {
        private readonly IFavoriteArtworkService _favoriteArtworkService;
        private readonly IArtworkService _artworkService;
        private readonly IMapper _mapper;

        public FavoriteArtworksController(IFavoriteArtworkService favoriteArtworkService, IMapper mapper, IArtworkService artworkService)
        {
            _favoriteArtworkService = favoriteArtworkService;
            _mapper = mapper;
            _artworkService = artworkService;
        }



        /*****************************************************************/
                   /*LIST OF ALL FAVORITE ARTWORKS BY HOBBYIST ID*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Get All Favorite Artworks By Hobbyist Id",
           Description = "Get All Favorite Artworks By Hobbyist Id",
           OperationId = "GetAllFavoriteArtworksByHobbyistId")]
        [SwaggerResponse(200, "Get All Favorite Artworks By Hobbyist Id", typeof(IEnumerable<ArtworkResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtworkResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<ArtworkResource>> GetAllByHobbyistIdAsync(long hobbyistId)
        {
            var artworks = await _artworkService.ListByHobbyistAsync(hobbyistId);
            var resources = _mapper.Map<IEnumerable<Artwork>, IEnumerable<ArtworkResource>>(artworks);
            return resources;
        }



        /*****************************************************************/
                             /*ASSIGN FAVORITE ARTWORK*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Assign Favorite Artwork",
           Description = "Assign Favorite Artwork",
           OperationId = "AssignFavoriteArtwork")]
        [SwaggerResponse(200, "Favorite Artwork Assigned", typeof(ArtworkResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ArtworkResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignFavoriteArtwork(long hobbyistId, long artworkId)
        {
            var result = await _favoriteArtworkService.AssignFavoriteArtworkAsync(hobbyistId, artworkId);
            if (!result.Success)
                return BadRequest(result.Message);
            var artworkResource = _mapper.Map<Artwork, ArtworkResource>(result.Resource.Artwork);
            return Ok(artworkResource);
        }



        /*****************************************************************/
                            /*UNASSIGN FAVORITE ARTWORK*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Unassign Favorite Artwork",
           Description = "Unassign Favorite Artwork",
           OperationId = "UnassignFavoriteArtwork")]
        [SwaggerResponse(200, "Favorite Artwork Unassigned", typeof(ArtworkResource))]

        [HttpDelete("{artworkId}")]
        [ProducesResponseType(typeof(ArtworkResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignFavoriteArtwork(long hobbyistId, long artworkId)
        {
            var result = await _favoriteArtworkService.UnassignFavoriteArtworkAsync(hobbyistId, artworkId);
            if (!result.Success)
                return BadRequest(result.Message);
            var favoriteArtworkResource = _mapper.Map<Artwork, ArtworkResource>(result.Resource.Artwork);
            return Ok(favoriteArtworkResource);
        }
    }
}
