using Games.Domain.Tags;
using General.Persistence.Stores;
using Microsoft.EntityFrameworkCore;

namespace Games.Persistence.Repositories {
    internal class TagsStore(DbContext context) : PaginableStore<Tag>(context), ITagsStore;
}
