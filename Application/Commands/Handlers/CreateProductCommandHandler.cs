using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductResponseDto>>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ProductResponseDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Nombre = request.Product.Nombre,
                Descripcion = request.Product.Descripcion,
                Precio = request.Product.Precio,
                Categoria = request.Product.Categoria,
                Estado = request.Product.Estado
            };

            await _repository.AddAsync(product);

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
