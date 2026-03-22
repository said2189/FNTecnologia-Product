using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductResponseDto>>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<ProductResponseDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product == null)
                return Response<ProductResponseDto>.Failure("Producto no encontrado");

            var dto = new ProductResponseDto
            {
                Id = product.Id,
                Nombre = product.Nombre,
                Descripcion = product.Descripcion,
                Precio = product.Precio,
                Categoria = product.Categoria,
                Estado = product.Estado
            };

            return Response<ProductResponseDto>.Success(dto);
        }
    }

}
