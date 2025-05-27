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

        public async Task<List<MessageVM>> GetMessagesAsync(string senderId, int groupId)
        {
            var allMessages = await GetAllMessagesAsync();

            Console.WriteLine($"[DEBUG] Loaded {allMessages.Count} messages");

            return allMessages;
        }

        public async Task<List<MessageVM>> GetPrivateMessagesAsync()
        {

            var response = await _apiSecurity.Http.GetAsync($"Message/GetPrivateMessage");
            if (!response.IsSuccessStatusCode) return new List<MessageVM>();

            var messages = await response.Content.ReadFromJsonAsync<List<MessageVM>>();
            return messages ?? new List<MessageVM>();
        }

        public async Task SendMessageAsync(CreatePrivateMessageCommand model)
        {
            var response = await _apiSecurity.Http.PostAsJsonAsync("Message/CreatePrivate", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to send private message.");
            }
        }
    }
}
