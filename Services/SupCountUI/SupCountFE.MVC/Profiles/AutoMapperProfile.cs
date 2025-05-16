using AutoMapper;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Application.Responses.Group;
using SupCountBE.Application.Responses.Justification;
using SupCountFE.MVC.ViewModels.Expense;
using SupCountFE.MVC.ViewModels.Group;

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
    }
}