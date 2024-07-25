namespace General.Domain.Results {
    public readonly record struct Error {
        public string Title { get; }
        public string Details { get; }

        private Error(string title, string details) {
            Title = title;
            Details = details;
        }

        public static Error Create(string title = "Error", string details = "No details") 
            => new(title, details);
    }
}
