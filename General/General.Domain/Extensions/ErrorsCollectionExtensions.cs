using General.Domain.Results;
using System.Net;

namespace General.Domain {
    public static class ErrorsCollectionExtensions {
        public static void Append(this ErrorsCollection source,
            string title = "Error", string description = "No details") {
            source.Append(Error.Create(title, description));
        }
    }
}
