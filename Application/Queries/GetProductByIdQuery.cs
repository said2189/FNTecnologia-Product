using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Response<ProductResponseDto>>;
}
