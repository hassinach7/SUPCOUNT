
using SupCountBE.Application.Responses;

namespace SupCountBE.Application.Mappers
{
    public class ReimbursementMapperProfile : Profile
    {
        public ReimbursementMapperProfile()
        {
            this.CreateMap<Reimbursement, ReimbursementResponse>();
        }
    }   
}
