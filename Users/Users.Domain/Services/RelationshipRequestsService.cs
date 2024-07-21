using General.Domain;
using Users.Domain.Relationships;
using Users.Domain.Relationships.Specifications;

namespace Users.Domain.Services {
    public class RelationshipRequestsService(
        IUserRelationshipsStore store) {
        public async Task<Result> SendRequest(string senderId, string targetId, CancellationToken token = default) {
            var specification = new FetchUserRelationship(senderId, targetId);
            var acceptorRelationship = await store.Fetch(specification, token);
            var acceptorSentRequest = acceptorRelationship != null;

            if (acceptorSentRequest) {
                acceptorRelationship.Accept();
                store.Update(acceptorRelationship, token);
            }

            var result = UserRelationshipRequest.Create(senderId, targetId);

            if (!result.IsSuccess) {
                return result.Error!;
            }

            var request = result.Value;

            if (acceptorSentRequest) {
                request.Accept();
            }

            store.Create(result.Value!, token);
            return Result.Success;
        }
    }
}
