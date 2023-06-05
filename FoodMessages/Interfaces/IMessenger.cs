using FoodMessages.Models;

namespace FoodMessages.Interfaces
{
    public interface IMessenger
    {
        Task SendMessage(MessageModel message);
    }
}
