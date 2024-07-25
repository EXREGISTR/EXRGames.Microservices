using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;
using System.Net;
using Users.Domain.Relationships.Specifications;
using Users.Domain.Services;

namespace Users.Domain.Relationships {
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

            return relationship;
        }

        public Result Accept() {
            if (Status != null) {
                return FailureResult.Create(
                    Error.Create(title: "Request already accepted"),
                    HttpStatusCode.Conflict);
            }

            Status = UserRelationshipStatus.Friend;
            return Result.Success;
        }

        public Result UpdateStatus(UserRelationshipStatus status) {
            if (Status == null) {
                return FailureResult.Create(
                    Error.Create(
                        title: "Update status error", 
                        details: "Impossible to update status if users has not relationship"), 
                    HttpStatusCode.Conflict);
            }

            Status = status;
            return Result.Success;
        }
    }

    public class SendRequest(
        IUserRelationshipsStore store,
        IUnitOfWork unitOfWork,
        RelationshipRequestsService service) {

        public async Task<Result> Handle(string senderId, string acceptorId, CancellationToken token = default) {
            if (senderId == acceptorId) return FailureResult.Create(
                Error.Create(title: "Incorrect ids", details: "Sender id is equal acceptor id"),
                HttpStatusCode.BadRequest);

            var specification = new FetchUserRelationship(senderId, acceptorId);
            var tempRelationshipResult = await store.Fetch(specification, token);
            if (tempRelationshipResult.IsFailure) return tempRelationshipResult.Error;

            var transaction = unitOfWork.BeginTransaction();

            try {
                await service.SendRequest(senderId, acceptorId, token);
                transaction.Commit();
                return Result.Success;
            } catch (Exception exception) {
                transaction.Rollback();
                return FailureResult.InternalServerError(exception.Message);
            }
        }
    }
}
