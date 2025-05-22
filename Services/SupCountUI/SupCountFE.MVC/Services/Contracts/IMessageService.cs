using SupCountFE.MVC.ViewModels.Message;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IMessageService
    {
        Task<List<MessageVM>> GetAllMessagesAsync();
        Task<MessageVM?> SendMessageAsync(CreateMessageVM model);
    }
}
