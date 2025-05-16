using AutoMapper;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Application.Responses.Group;
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

    }
    }