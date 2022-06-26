using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;


namespace QTShop.Inventory.Repositories
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly string connectionString;

        public ProductInventoryRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateProductInInventory(string productId, string productName)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"INSERT INTO dbo.ProductInventory (ProductId, ProductName, Quantity) VALUES ('{productId}', '{productName}', 0)";
                await db.ExecuteAsync(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
    }
}