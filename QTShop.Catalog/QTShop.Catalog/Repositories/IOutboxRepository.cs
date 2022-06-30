using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTShop.Catalog.Model;

namespace QTShop.Catalog.Repositories
{
    public interface IOutboxRepository
    {
       Task CreateOutboxMessage(OutboxMessage outboxMessage);
        Task UpdateOutboxMessageState(string eventId, OutboxMessageState state);
        Task<List<OutboxMessage>> GetAllReadyToSend();
    }
}