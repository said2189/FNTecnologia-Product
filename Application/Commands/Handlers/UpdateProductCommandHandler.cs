using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductResponseDto>>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ProductResponseDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                return Response<ProductResponseDto>.Failure("Product not found");

            product.Nombre = request.Product.Nombre;
            product.Descripcion = request.Product.Descripcion;
            product.Precio = request.Product.Precio;
            product.Categoria = request.Product.Categoria;
            product.Estado = request.Product.Estado;

            await _repository.UpdateAsync(product);

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
