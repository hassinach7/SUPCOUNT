using AutoMapper;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Message;
using System.Net.Http;

namespace SupCountFE.MVC.Services.Implementations;

    public class MessageService : IMessageService
    {
        private readonly ApiSecurity _apiSecurity;
       
    public MessageService(ApiSecurity apiSecurity)
    {
        _apiSecurity = apiSecurity;
    
    }

    
        public async Task<List<MessageVM>> GetAllMessagesAsync()
        {
        var response = await _apiSecurity.Http.GetAsync("api/Message/GetAll");
        if (!response.IsSuccessStatusCode) return new List<MessageVM>();
        var data = await response.Content.ReadFromJsonAsync<List<MessageVM>>();
        return data ?? new List<MessageVM>();
    }

        public async Task<MessageVM?> SendMessageAsync(CreateMessageVM model)
        {
        var sendModel = new
        {
            Content = model.Content!,
            SenderId = model.SenderId!,
            RecipientId = model.RecipientId!,
            GroupId = model.GroupId!
        };

        var response = await _apiSecurity.Http.PostAsJsonAsync("api/Message/Create", sendModel);
        if (!response.IsSuccessStatusCode) return null;

        var created = await response.Content.ReadFromJsonAsync<MessageVM>();
        return created;
    }
}
    

