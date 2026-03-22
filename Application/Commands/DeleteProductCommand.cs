using Application.Common;
using MediatR;

namespace Application.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Response<bool>>;
}
