﻿using Games.Contracts.Requests.Games;
using General.API;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Games.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(ISender sender) : ControllerBase {
        [HttpGet("FetchById")]
        public async Task<IResult> FetchGame(
            [FromQuery] FetchGameQuery request,
            CancellationToken token) 
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpGet]
        public async Task<IResult> FetchGames(
            [FromQuery] FetchGamesQuery request,
            CancellationToken token) 
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpPost("Create")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IResult> CreateGame(
            [FromBody] CreateGameCommand request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpPut("Update")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IResult> UpdateGame(
            [FromBody] UpdateGameCommand request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpDelete("Delete")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IResult> DeleteGame(
            [FromBody] DeleteGameCommand request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));
    }
}
