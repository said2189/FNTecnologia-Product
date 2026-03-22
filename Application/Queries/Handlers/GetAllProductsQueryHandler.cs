using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<IEnumerable<ProductResponseDto>>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<ProductResponseDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            var dtoList = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Categoria = p.Categoria,
                Estado = p.Estado
            });

            return Response<IEnumerable<ProductResponseDto>>.Success(dtoList);
        }
    }
}
