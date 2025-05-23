using AutoMapper;
using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Responses.Category;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Application.Responses.Group;
using SupCountBE.Application.Responses.Justification;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountBE.Application.Responses.User;
using SupCountBE.Application.Responses.UserGroup;
using SupCountFE.MVC.ViewModels.Category;
using SupCountFE.MVC.ViewModels.Expense;
using SupCountFE.MVC.ViewModels.Group;
using SupCountFE.MVC.ViewModels.Message;
using SupCountFE.MVC.ViewModels.Reimbursement;
using SupCountFE.MVC.ViewModels.User;
using SupCountFE.MVC.ViewModels.UserGroup;

namespace SupCountFE.MVC.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        this.CreateMap<GroupResponse, UpdateGroupVM>();
        this.CreateMap<ExpenseResponse, UpdateExpenseVM>();
        this.CreateMap<ExpenseResponse, CreateExpenseVM>();
        this.CreateMap<CreateExpenseVM, ExpenseResponse>();
        this.CreateMap<ExpenseResponse, ExpenseVM>();
        this.CreateMap<ExpenseGroupResponse, ExpenseGroupVM>();
        this.CreateMap<JustificationResponse, JustificationExepnseVM>();
        this.CreateMap<CategoryResponse, CategoryVM>();
        this.CreateMap<GroupResponse, GroupVM>();
        this.CreateMap<UserResponse, UpdateUserVM>();
        this.CreateMap<UpdateUserVM, UserResponse>();
        this.CreateMap<UserResponse, RegisterUserVM>();
        this.CreateMap<RegisterUserVM, UserResponse>();
        this.CreateMap<UserResponse, UserVM>();
        this.CreateMap<UserGroupResponse, UserGroupVM>();
        this.CreateMap<SoldeUserResponse, SoldeUserVM>();
        this.CreateMap<ReimbursementResponse, ReimbursementVM>();
        this.CreateMap<MessageResponse, MessageVM>();

        this.CreateMap<CreateMessageVM, CreateMessageCommand>();


    }
}