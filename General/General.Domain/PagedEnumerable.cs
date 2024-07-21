namespace General.Domain {
    public record struct PagedEnumerable<TSource>(
        IEnumerable<TSource> Source, 
        int Page, 
        int Size, 
        int TotalCount);
}
