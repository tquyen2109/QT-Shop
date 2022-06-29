using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using QTShop.Category.Model;

namespace QTShop.Category.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly string connectionString;
        public OutboxRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task CreateOutboxMessage(OutboxMessage outboxMessage)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"INSERT INTO dbo.OutboxMessage (Data, Type, EventId,EventDate,State, ModifiedDate) " +
                    $"VALUES ('{outboxMessage.Data}', '{outboxMessage.Type}', '{outboxMessage.EventId}','{outboxMessage.EventDate}','{outboxMessage.State}','{outboxMessage.ModifiedDate}')";
                await db.ExecuteAsync(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateOutboxMessageState(string eventId, OutboxMessageState state)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"Update dbo.OutboxMessage set State = '{state}' where EventId = '{eventId}'";
                await db.ExecuteAsync(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<OutboxMessage>> GetAllReadyToSend()
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"SELECT * FROM dbo.OutboxMessage WHERE State = 'ReadyToSend'";
                var result = await db.QueryAsync<OutboxMessage>(sql);
                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}