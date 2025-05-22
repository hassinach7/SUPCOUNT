using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            IUserService userService  ,
            IMapper mapper)
        {
            _messageService = messageService;
            _helper = helper;
            _groupService = groupService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateMessageVM
            {
                SenderId = _helper.UserId!
            };

           
            ViewBag.UsersItems = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName");
            ViewBag.GroupsItems = new SelectList(await _groupService.GetAllGroupsAsync(), "Id", "Name");
            ViewBag.Messages = await _messageService.GetAllMessagesAsync();

            return View(model); 
        }

        private async Task<MessageVM> FillListe(MessageVM model)
        {
            var users = await _userService.GetAllUsersAsync();
            var groups = await _groupService.GetAllGroupsAsync();

            model.UsersItems = new SelectList(users.Where(u => u.Id != _helper.UserId), "Id", "FullName");
            model.GroupsItems = new SelectList(groups, "Id", "Name");

            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MessageVM model)
        {
            if (!ModelState.IsValid)
            {
                model = await FillListe(model);
                ViewBag.Messages = await _messageService.GetAllMessagesAsync();
                return View(model);
            }

            model.SenderId = _helper.UserId!;

            var createModel = _mapper.Map<CreateMessageVM>(model);

            var result = await _messageService.SendMessageAsync(createModel);
            if (result != null)
            {
                TempData["Success"] = "Message sent successfully!";
                return RedirectToAction(nameof(Create));
            }

            ModelState.AddModelError("", "Failed to send the message.");
            model = await FillListe(model);
            ViewBag.Messages = await _messageService.GetAllMessagesAsync();
            return View(model);
        }


    }
}
