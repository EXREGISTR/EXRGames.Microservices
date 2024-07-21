using MediatR;

namespace General.Contracts {
    public interface ICommand : IRequest;
    public interface ICommand<TResponse> : IRequest<TResponse>;
}
