using SupCountBE.Application.Responses.Transaction;


namespace SupCountBE.Application.Mappers
{
    public class TransactionMapperProfile : Profile
    { 
        public TransactionMapperProfile()
        {
            CreateMap<Transaction, TransactionResponse>()
                .ForMember(dest => dest.ReimbursementName, opt => opt.MapFrom(src => src.Reimbursement!.Name));
        }
    }
}
