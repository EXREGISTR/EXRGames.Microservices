using MediatR;

namespace General.Contracts {
    public interface ICommand<TResponse> : IRequest<TResponse>;
}
