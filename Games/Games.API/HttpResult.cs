using General.Domain.Results;

namespace Games.API {
    public static class HttpResult {
        public static IResult FromResult<T>(Result<T> result) {
            if (result.IsSuccess) {
                return TypedResults.Ok(result.Value);
            }

            var error = result.Error;
            var extensions = new Dictionary<string, object?>();
            foreach (var message in error.Messages) {
                extensions.Add(message.Title, message.Details);
            }

            return Results.Problem(
                statusCode: (int)error.StatusCode,
                title: "Errors",
                extensions: extensions);
        }

        public static IResult FromResult(Result result) {
            if (result.IsSuccess) {
                return Results.Ok();
            }

            var error = result.Error;
            var extensions = new Dictionary<string, object?>();
            foreach (var message in error.Messages) {
                extensions.Add(message.Title, message.Details);
            }

            return Results.Problem(
                statusCode: (int)error.StatusCode,
                title: "Errors",
                extensions: extensions);
        }
    }
}
