using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetAllProductsQuery() : IRequest<Response<IEnumerable<ProductResponseDto>>>;
}
