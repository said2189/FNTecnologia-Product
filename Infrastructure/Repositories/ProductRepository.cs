using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Infrastructure.Persistence;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ProductRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task AddAsync(Product product)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "CreateProduct",
                new
                {
                    product.Id,
                    product.Nombre,
                    product.Descripcion,
                    product.Precio,
                    product.Categoria,
                    product.Estado
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task UpdateAsync(Product product)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "UpdateProduct",
                new
                {
                    product.Id,
                    product.Nombre,
                    product.Descripcion,
                    product.Precio,
                    product.Categoria,
                    product.Estado
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task DeleteAsync(Guid id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "DeleteProduct",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Product>(
                "GetProductById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Product>(
                "GetAllProducts",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
