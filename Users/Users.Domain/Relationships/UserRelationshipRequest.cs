using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Users.Domain.Relationships
{
    public class UserRelationshipRequest : Entity, IAggregateRoot {
        public string SenderId { get; private set; } = string.Empty;
        public string AcceptorId { get; private set; } = string.Empty;
        public UserRelationshipStatus? Status { get; private set; }

        public UserProfile SenderProfile { get; private set; } = null!;
        public UserProfile AcceptorProfile { get; private set; } = null!;

        private UserRelationshipRequest() { }

        internal static Result<UserRelationshipRequest> Create(string senderId, string acceptorId) {
            var relationship = new UserRelationshipRequest {
                SenderId = senderId,
                AcceptorId = acceptorId
            };

            return Result<UserRelationshipRequest>.Success(relationship);
        }

        public Result Accept() {
            if (Status != null) {
                return "Request already accepted";
            }

            Status = UserRelationshipStatus.Friend;
            return Result.Success;
        }

        public Result UpdateStatus(UserRelationshipStatus status) {
            if (Status == null) {
                return "Impossible to update status if users has not relationship";
            }

            Status = status;
            return Result.Success;
        }
    }

    public class SendRequest(
        IUserRelationshipsStore store,
        IUnitOfWork unitOfWork) {

        public async Task Handle(string senderId, string acceptorId, CancellationToken token = default) {
            if (senderId == acceptorId) return;

            var tempRelationship = await store.Fetch(senderId, acceptorId, token);
            if (tempRelationship != null) return;

            var transaction = unitOfWork.BeginTransaction();

            try {
                await CreateFriends(senderId, acceptorId, token);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
            }
        }

        private async Task CreateFriends(string senderId, string targetId, CancellationToken token) {
            var acceptorRelationship = await store.Fetch(targetId, senderId, token);
            var acceptorSentRequest = acceptorRelationship != null;

            if (acceptorSentRequest) {
                acceptorRelationship.Accept();
                store.Update(acceptorRelationship, token);
                await unitOfWork.SaveChanges(token);
            }

            var result = UserRelationshipRequest.Create(senderId, targetId);

            if (!result.IsSuccess) {
                throw new Exception(result.Error);
            }

            var request = result.Value;

            if (acceptorSentRequest) {
                request.Accept();
            }

            store.Create(result.Value!, token);
            await unitOfWork.SaveChanges(token);
        }
    }
}
