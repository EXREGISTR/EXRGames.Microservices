using Games.Contracts.Requests.Games;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Games.API.Controllers {
    [Route("games")]
    [ApiController]
    public class GamesController(IMediator mediator) : ControllerBase {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateGameCommand request, CancellationToken token) {
            var result = await mediator.Send(request, token);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("all")]
        public async Task<IActionResult> FetchAll([FromQuery] FetchGamesQuery request, CancellationToken token) {
            var games = await mediator.Send(request, token);
            return Ok(games);
        }
    }
}
