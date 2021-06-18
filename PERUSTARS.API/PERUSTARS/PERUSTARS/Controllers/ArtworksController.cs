using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PERUSTARS.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PERUSTARS.Domain.Models;
using PERUSTARS.Resources;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;
using PERUSTARS.Extensions;


namespace PERUSTARS.Controllers
{
    [ApiController]
    [Route("/api/artists/{artistId}/artworks")]
    [Produces("application/json")]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        private readonly IMapper _mapper;

        public ArtworksController(IArtworkService artowrkService, IMapper mapper)
        {
            _artworkService = artowrkService;
            _mapper = mapper;
        }



        /*****************************************************************/
                           /*LIST OF ARTWORKS BY ARTIST ID*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "List Artworks By Artist Id",
         Description = "List Artworks By Artist Id",
         OperationId = "ListArtworksByArtistId")]
        [SwaggerResponse(200, "List of Artworks By Artist Id", typeof(IEnumerable<ArtworkResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtworkResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<ArtworkResource>> GetAllByArtistIdAsync(long artistId)
        {
            var artworks = await _artworkService.ListByArtistIdAsync(artistId);
            var resources = _mapper.Map<IEnumerable<Artwork>, IEnumerable<ArtworkResource>>(artworks);
            return resources;
        }



        /*****************************************************************/
                                /*GET ARTWORK BY ID*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Get Artwork by Id",
         Description = "Get Artwork by Id",
         OperationId = "GetArtworkById")]
        [SwaggerResponse(200, "Get Artwork by Id", typeof(ArtworkResource))]

        [HttpGet("{artworkId}")]
        [ProducesResponseType(typeof(ArtworkResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(long artworkId, long artistId)
        {
            var result = await _artworkService.GetByIdAndArtistIdAsync(artworkId, artistId);
            if (!result.Success)
                return BadRequest(result.Message);
            var artworkResource = _mapper.Map<Artwork, ArtworkResource>(result.Resource);
            return Ok(artworkResource);
        }



        /*****************************************************************/
                                    /*SAVE ARTWORK*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Save Artwork",
         Description = "Save Artwork",
         OperationId = "SaveArtwork")]
        [SwaggerResponse(200, "Save Artwork", typeof(ArtworkResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ArtworkResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(long artistId, [FromBody] SaveArtworkResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var artwork = _mapper.Map<SaveArtworkResource, Artwork>(resource);
            var result = await _artworkService.SaveAsync(artistId, artwork);

            if (!result.Success)
                return BadRequest(result.Message);
            var artworkResource = _mapper.Map<Artwork, ArtworkResource>(result.Resource);
            return Ok(artworkResource);

        }



        /*****************************************************************/
                                /*UPDATE ARTWORK*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Update Artwork",
         Description = "Update Artwork",
         OperationId = "UpdateArtwork")]
        [SwaggerResponse(200, "Update Artwork", typeof(ArtworkResource))]

        [HttpPut("{artworkId}")]
        [ProducesResponseType(typeof(ArtworkResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(long artworkId, long artistId, [FromBody] SaveArtworkResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var artwork = _mapper.Map<SaveArtworkResource, Artwork>(resource);
            var result = await _artworkService.UpdateAsync(artworkId, artistId, artwork);

            if (!result.Success)
                return BadRequest(result.Message);

            var artworkResource = _mapper.Map<Artwork, ArtworkResource>(result.Resource);
            return Ok(artworkResource);
        }



        /*****************************************************************/
                                /*DELETE ARTWORK*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Delete Artwork",
         Description = "Delete Artwork",
         OperationId = "DeleteArtwork")]
        [SwaggerResponse(200, "Delete Artwork", typeof(ArtworkResource))]

        [HttpDelete("{artworkId}")]
        [ProducesResponseType(typeof(ArtworkResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(long artworkId, long artistId)
        {
            var result = await _artworkService.DeleteAsync(artworkId, artistId);
            if (!result.Success)
                return BadRequest(result.Message);
            var artworkResource = _mapper.Map<Artwork, ArtworkResource>(result.Resource);
            return Ok(artworkResource);
        }
    }
}
