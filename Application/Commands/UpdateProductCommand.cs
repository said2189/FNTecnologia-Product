using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public record UpdateProductCommand(Guid Id, ProductRequestDto Product) : IRequest<Response<ProductResponseDto>>;
}
