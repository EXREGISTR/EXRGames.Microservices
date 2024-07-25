using General.Domain.Contracts;
using General.Domain.Results;
using Users.Domain.Relationships;
using Users.Domain.Relationships.Specifications;

namespace Users.Domain.Services {
    public class RelationshipRequestsService(
        IUserRelationshipsStore store,
        IUnitOfWork unitOfWork) {
        public async Task<Result> SendRequest(string senderId, string acceptorId, CancellationToken token = default) {
            var specification = new FetchUserRelationship(acceptorId, senderId);
            var acceptorRelationship = await store.Fetch(specification, token);
            var acceptorSentRequest = acceptorRelationship.IsSuccess;

            if (acceptorSentRequest) {
                acceptorRelationship.Value.Accept();
                store.Update(acceptorRelationship.Value);
            }

            var result = UserRelationshipRequest.Create(senderId, acceptorId);

            if (result.IsFailure) return result.Error;

            var request = result.Value;

            if (acceptorSentRequest) {
                request.Accept();
            }

            store.Create(result.Value!);

            await unitOfWork.SaveChanges(token);
            return Result.Success;
        }
    }
}
