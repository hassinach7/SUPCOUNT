using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupCountBE.Application.Commands.Message;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Message;

namespace SupCountFE.MVC.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly Helper _helper;
        private readonly IMapper _mapper;

        public MessageController(
            IMessageService messageService,
            Helper helper,
            IGroupService groupService,
            IUserService userService,
            IMapper mapper)
        {
            _messageService = messageService;
            _helper = helper;
            _groupService = groupService;
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Json(messages);
        }



        [HttpGet]
        public async Task<IActionResult> Create(string? recipientId, int? groupId)
        {
            var userId = _helper.UserId!;

            var model = new CreateMessageVM
            {
                SenderId = userId,
                RecipientId = recipientId,
                GroupId = groupId,
                UsersItems = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName"),
                GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name"),
                Messages = await _messageService.GetMessagesAsync(userId, recipientId, groupId)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageVM model)
        {
            if (!ModelState.IsValid)
            {
                model.UsersItems = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName");
                model.GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name");
                model.Messages = await _messageService.GetMessagesAsync(model.SenderId, model.RecipientId, model.GroupId);
                return View(model);
            }

            model.SenderId = _helper.UserId!;

            var command = _mapper.Map<CreateMessageCommand>(model);

            var result = await _messageService.SendMessageAsync(command);
            if (result != null)
            {
                TempData["Success"] = "Message sent successfully!";
                return RedirectToAction(nameof(Create), new { recipientId = model.RecipientId, groupId = model.GroupId });
            }

            ModelState.AddModelError("", "Failed to send the message.");
            model.UsersItems = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName");
            model.GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name");
            model.Messages = await _messageService.GetMessagesAsync(model.SenderId, model.RecipientId, model.GroupId);
            return View(model);
        }
    }
}
