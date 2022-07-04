using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace QTShop.Order.Query
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task CreateOrder(CreateOrderRequest request)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"INSERT INTO dbo.[Order] (OrderId, Total, OrderStatus) VALUES ('{request.OrderId}', {request.Total}, '{request.OrderStatus}')";
                await db.ExecuteAsync(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public class CreateOrderRequest
        {
            public string OrderId { get; set; }
            public string OrderStatus { get; set; }
            public int Total { get; set; }
        }
    }
}