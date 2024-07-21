namespace General.Contracts {
    public abstract class PaginableQuery {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = int.MaxValue;
    }
}
