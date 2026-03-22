using Application.Common;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<bool>>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                return Response<bool>.Failure("Product not found");

            await _repository.DeleteAsync(request.Id);
            return Response<bool>.Success(true);
        }
    }
}
