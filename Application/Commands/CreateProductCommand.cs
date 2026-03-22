using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public record CreateProductCommand(ProductRequestDto Product) : IRequest<Response<ProductResponseDto>>;
}
