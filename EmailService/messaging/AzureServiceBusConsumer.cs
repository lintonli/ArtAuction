﻿using Azure.Messaging.ServiceBus;
using EmailService.Models;
using EmailService.Service;
using Newtonsoft.Json;
using System.Text;

namespace EmailService.messaging
{
    public class AzureServiceBusConsumer:IAzureBus
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusProcessor _mailProcessor;
        private readonly ServiceBusProcessor _orderServiceProcessor;
        private readonly MailService _mailService;
        public AzureServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("AzureConnectionString");
            _queueName = _configuration.GetValue<string>("QueuesAndTopics:RegisterQueue");

            var client = new ServiceBusClient(_connectionString);
            _mailProcessor = client.CreateProcessor(_queueName);
            _mailService = new MailService(configuration);

        }
        public async Task start()
        {
            _mailProcessor.ProcessMessageAsync += OnRegisterUser;
            _mailProcessor.ProcessErrorAsync += ErrorHandler;
            await _mailProcessor.StartProcessingAsync();
        }
        public async Task stop()
        {
            await _mailProcessor.StopProcessingAsync();
            await _mailProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }

        private async Task OnRegisterUser(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var user = JsonConvert.DeserializeObject<UserMessageDto>(body);
            try
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://i.pinimg.com/564x/4b/85/96/4b859696e1705b60b49a5deaf05e0350.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + user.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to our Website");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Start your First Artistic Adventure with Edds!!</p>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Every Bidder is a Winner and every bid Counts</p>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>You are Just a bid away!</p>");
                await _mailService.sendEmail(user, stringBuilder.ToString());
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
