using Oid85.FinMarket.TraderTinkoff.Core.Models;

namespace Oid85.FinMarket.TraderTinkoff.Core.Responses
{
    public class OutboxTaskListResponse
    {
        public List<OutboxTask> Tasks { get; set; } = [];
    }
}
