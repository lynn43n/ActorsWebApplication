using ActorsWebApplication.Data.Interfaces;
using ActorsWebApplication.DTO;
using ActorsWebApplication.Models;
using ActorsWebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using System.Threading.Tasks;

namespace ActorsWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        readonly IActorsService _actorService;
        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Retrieves a list of all actors.
        /// </summary>
        /// <response code="200">Returns the list of actors.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ActorsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> GetActors(string actorName = null,
            int? minRank = null,
            int? maxRank = null,
            string provider = null,
            int skip = 0,
            int take = 20)
        {
            return await HandleRequest(async () =>
            {
                var actors = await _actorService.GetActors(actorName, minRank, maxRank, provider, skip, take);
                return new ActorsResponse(200, true, actors);
            });
        }
        /// <summary>
        /// Retrieves details for a specific actor
        /// </summary>
        /// <response code="200">Returns Actor.</response>
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Get(string id)
        {
            return await HandleRequest(async () =>
            {
                var actor = await _actorService.Get(id);
                return new ActorResponse(200, true, actor);
            });
        }
        /// <summary>
        /// Add a specific actor
        /// </summary>
        /// <response code="200">Returns the added Actor.</response>
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        [HttpPost("{id}")]
        public async Task<ActionResult<Response>> Post(string id, [FromBody] UpsertRequest value)
        {
            return await HandleRequest(async () =>
            {
                ActorModel actor = value;
                actor.Id = id;
                await _actorService.Add(actor);
                return new ActorResponse(200, true, actor);
            });
        }
        /// <summary>
        /// Updates a specific actor
        /// </summary>
        /// <response code="200">Returns the updated Actor.</response>
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Put(string id, [FromBody] UpsertRequest value)
        {
            return await HandleRequest(async () =>
            {
                ActorModel actor = value;
                actor.Id = id;
                await _actorService.Put(actor);
                return new ActorResponse(200, true, actor);
            });
        }
        /// <summary>
        /// Delete a specific actor
        /// </summary>
        /// <response code="200">Returns the deleted Actor.</response>
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(string id)
        {
            return await HandleRequest(async () =>
            {
                var actor = await _actorService.Delete(id);
                return new ActorResponse(200, true, actor);
            });
        }

        private async Task<ActionResult<Response>> HandleRequest(Func<Task<Response>> action)
        {
            try
            {
                var response = await action();
                return StatusCode(response.StatusCode, response);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(404, new Response(404, false, [new Error { Message = ex.Message, Code = ex.Message }]));
            }
            catch (DuplicateRankException ex)
            {
                return StatusCode(400, new Response(400, false, [new Error { Message = ex.Message, Code = ex.Message }]));               
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(500, false, [new Error { Message = ex.Message, Code = ex.Message }]));                
            }
        }
    }
}
