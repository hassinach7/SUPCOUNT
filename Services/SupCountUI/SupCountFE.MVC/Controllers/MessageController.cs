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
            return View(messages);
        }
        [HttpGet]
        public async Task<IActionResult> Private(string recipientId)
        {
            var userId = _helper.UserId!;
            var messages = await _messageService.GetPrivateMessagesAsync(userId, recipientId);
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users
                .Where(u => u.Id != userId)
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.FullName
                }).ToList();
            ViewBag.SenderId = userId;

            return View(messages);
        }


        [HttpPost]
        public async Task<IActionResult> SendPrivate(CreateMessageCommand model)
        {
            model.SenderId = _helper.UserId!;
            model.IsPrivate = true;

            if (string.IsNullOrWhiteSpace(model.Content) || string.IsNullOrEmpty(model.RecipientId))
            {
                ModelState.AddModelError("", "Message or recipient missing.");
                return RedirectToAction("Private", new { recipientId = model.RecipientId });
            }

            var result = await _messageService.SendMessageAsync(model);

            return RedirectToAction("Private", new { recipientId = model.RecipientId });
        }


        [HttpGet]
        public async Task<IActionResult> Create(int? groupId)
        {
            var userId = _helper.UserId!;
            if (groupId == null)
            {
                ModelState.AddModelError("", "Group ID is required.");
                return View(new CreateMessageVM { GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name") });
            }

            var model = new CreateMessageVM
            {
                SenderId = userId,
                GroupId = groupId,
                GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name"),
                Messages = await _messageService.GetMessagesAsync(userId, groupId.Value)
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageVM model)
        {

            if (model.GroupId == null)
            {
                ModelState.AddModelError("", "Group ID is required.");
                model.GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name");
                return View(model);
            }
            model.SenderId = _helper.UserId!; 



            if (!ModelState.IsValid)
            {
                model.GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name");
                model.Messages = await _messageService.GetMessagesAsync(model.SenderId, model.GroupId.Value);
                return View(model);
            }

            var command = _mapper.Map<CreateMessageCommand>(model);
            var result = await _messageService.SendMessageAsync(command);

            if (result != null)
            {
                TempData["Success"] = "Message sent successfully!";
                return RedirectToAction(nameof(Create), new { groupId = model.GroupId });
            }

            ModelState.AddModelError("", "Failed to send the message.");
            model.GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name");
            model.Messages = await _messageService.GetMessagesAsync(model.SenderId, model.GroupId.Value);
            return View(model);
        }

    }
}
