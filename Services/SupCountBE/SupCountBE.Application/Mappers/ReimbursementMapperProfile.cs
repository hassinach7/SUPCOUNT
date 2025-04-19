
using SupCountBE.Application.Responses;
using SupCountBE.Application.Responses.Reimbursement;

namespace SupCountBE.Application.Mappers
{
    public class ReimbursementMapperProfile : Profile
    {
        public ReimbursementMapperProfile()
        {
            CreateMap<Reimbursement, ReimbursementResponse>()
          .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender!.FullName))
          .ForMember(dest => dest.BeneficiaryName, opt => opt.MapFrom(src => src.Beneficiary!.FullName))
          .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group!.Name))
          .ForMember(dest => dest.TransactionCount, opt => opt.MapFrom(src => src.Transactions != null ? src.Transactions.Count : 0));
        }
    }   
}
