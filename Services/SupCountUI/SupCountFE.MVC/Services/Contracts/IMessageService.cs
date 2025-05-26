using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Responses.Message;
using SupCountFE.MVC.ViewModels.Message;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IMessageService
    {
        Task<List<MessageVM>> GetAllMessagesAsync();
        Task<MessageResponse?> SendMessageAsync(CreateMessageCommand model);
        Task<List<MessageVM>> GetMessagesAsync(string senderId, string? recipientId, int? groupId);
        Task<List<MessageVM>> GetPrivateMessagesAsync(string senderId, string? recipientId);
    }
}