using Games.Contracts.Requests.Tags;
using General.API;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Games.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController(ISender sender) : ControllerBase {
        [HttpGet]
        public async Task<IResult> FetchTags(
            [FromQuery] FetchTagsQuery request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpPost("Create")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IResult> Create(
            [FromBody] CreateTagCommand request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpPut("Update")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IResult> Update(
            [FromBody] UpdateTagCommand request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));

        [HttpDelete("Delete")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IResult> Delete(
            [FromBody] DeleteTagCommand request,
            CancellationToken token)
            => HttpResult.FromResult(await sender.Send(request, token));
    }
}
