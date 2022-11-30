using System;
using System.Text.Json.Serialization;

namespace WindTest.EventBus.Events
{
    public class ServiceEvent
    {
        public ServiceEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public ServiceEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonInclude]
        public Guid Id { get; }

        [JsonInclude]
        public DateTime CreationDate { get; }
    }
}