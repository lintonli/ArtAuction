using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionMessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly string connectionString = "Endpoint=sb://lintauctionservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=qc5X/e62DmAVdWXfPhuVdUyMUMdCDL/DA+ASbCKLScw=";
        public async Task PublishMessage(object message, string Topic_queue_Name)
        {
            var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(Topic_queue_Name);

            var body = JsonConvert.SerializeObject(message);
            ServiceBusMessage myMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await sender.SendMessageAsync(myMessage);
            await sender.DisposeAsync();

        }
    }
}
