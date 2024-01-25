using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionMessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(object message, string Topic_queue_Name);
    }
}
