using FoodMessages.Exceptions;
using FoodMessages.Interfaces;
using FoodMessages.Models;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FoodMessages.Messenger
{
    public class MessengerTwillio : IMessenger
    {
        private readonly IOptions<ConfigTwillio> _configTwillio;

        public MessengerTwillio(IOptions<ConfigTwillio> configTwillio)
        {
            _configTwillio = configTwillio;
        }

        public async Task SendMessage(MessageModel messageModel)
        {
            var config = _configTwillio.Value;

            TwilioClient.Init(
                config.SID,
                config.Token
            );

            var message = await MessageResource.CreateAsync(
                body: messageModel.Body,
                from: new Twilio.Types.PhoneNumber(config.DefaultNumber),
                to: new Twilio.Types.PhoneNumber(messageModel.To));

            if(message.Status != MessageResource.StatusEnum.Delivered)
            {
                throw new MessageNotDeliveredException();
            }
        }
    }
}
