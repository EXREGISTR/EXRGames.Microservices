using General.Domain.Specifications;

namespace Users.Domain.Relationships.Specifications
{
    public class FetchUserRelationshipsByAcceptorId : Specification<UserRelationshipRequest> {
        public FetchUserRelationshipsByAcceptorId(string acceptorId, UserRelationshipStatus? status = null) {
            Where(x => x.AcceptorId == acceptorId); 
            
            if (status != null) {
                Where(x => x.Status == status);
            }
        }
    }
}
