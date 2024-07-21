using General.Domain.Specifications;

namespace Users.Domain.Relationships.Specifications
{
    public class FetchUserRelationship : Specification<UserRelationshipRequest> {
        public FetchUserRelationship(string senderId, string acceptorId, UserRelationshipStatus? status = null) {
            Where(x => x.SenderId == senderId && x.AcceptorId == acceptorId);

            if (status != null) {
                Where(x => x.Status == status);
            }
        }
    }
}
