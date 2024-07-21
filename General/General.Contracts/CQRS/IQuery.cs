using MediatR;

namespace General.Contracts {
    public interface IQuery<TResponse> : IRequest<TResponse>;
}
