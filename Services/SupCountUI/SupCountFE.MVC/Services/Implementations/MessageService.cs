using SupCountBE.Application.Responses.Message;
using SupCountBE.Application.Commands.Message;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Message;

namespace SupCountFE.MVC.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ApiSecurity _apiSecurity;

        public MessageService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
        }

        public async Task<List<MessageVM>> GetAllMessagesAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("Message/GetAll");
            if (!response.IsSuccessStatusCode) return new List<MessageVM>();
            var data = await response.Content.ReadFromJsonAsync<List<MessageVM>>();
            return data ?? new List<MessageVM>();
        }

        public async Task<MessageResponse?> SendMessageAsync(CreateMessageCommand model)
        {
            var response = await _apiSecurity.Http.PostAsJsonAsync("Message/Create", model);
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<MessageResponse>();
            return result;
        }

        public async Task<List<MessageVM>> GetMessagesAsync(string senderId, string? recipientId, int? groupId)
        {
            var allMessages = await GetAllMessagesAsync();

            Console.WriteLine($"[DEBUG] Loaded {allMessages.Count} messages");

            if (groupId != null)
            {
                var groupMessages = allMessages.Where(m => m.GroupId == groupId).ToList();
                Console.WriteLine($"[DEBUG] Group messages: {groupMessages.Count}");
                return groupMessages;
            }

            if (!string.IsNullOrEmpty(recipientId))
            {
                var privateMessages = allMessages
                    .Where(m =>
                        (m.SenderId == senderId && m.RecipientId == recipientId) ||
                        (m.SenderId == recipientId && m.RecipientId == senderId))
                    .ToList();
                Console.WriteLine($"[DEBUG] Private messages: {privateMessages.Count}");
                return privateMessages;
            }

            return new List<MessageVM>();
        }

    }
}
