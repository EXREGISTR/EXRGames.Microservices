using General.Domain.Specifications;

namespace Users.Domain.Relationships.Specifications
{
    public class FetchUserRelationshipsBySenderId : Specification<UserRelationshipRequest> {
        public FetchUserRelationshipsBySenderId(string senderId, UserRelationshipStatus? status = null) {
            Where(x => x.SenderId == senderId);
            if (status != null) {
                Where(x => x.Status == status);
            }
        }

        public void WhereAcceptorId(string acceptorId) => Where(x => x.AcceptorId == acceptorId);
    }
}
