using System;
using System.Text.Json;


namespace QTShop.Catalog.Model
{
    public class OutboxMessage
    {
        public OutboxMessage()
        {
            
        }
        public OutboxMessage(object message, string eventId, DateTime eventDate)
        {
            Data = JsonSerializer.Serialize(message);
            Type = message.GetType().FullName + ", " +
                   message.GetType().Assembly.GetName().Name;
            EventId = eventId;
            EventDate = eventDate;
            State = OutboxMessageState.ReadyToSend;
            ModifiedDate = DateTime.Now;
        }
        public string Data { get; set; }
        public string Type { get; protected set; }
        public string EventId { get;  set; }
        public DateTime EventDate { get;  set; }
        public OutboxMessageState State { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public void ChangeState(OutboxMessageState state)
        {
            State = state;
            this.ModifiedDate = DateTime.Now;
        }
        
    }
}