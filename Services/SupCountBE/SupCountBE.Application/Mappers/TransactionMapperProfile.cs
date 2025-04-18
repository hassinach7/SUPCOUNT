using SupCountBE.Application.Responses.Transaction;


namespace SupCountBE.Application.Mappers
{
    public class TransactionMapperProfile : Profile
    { 
        public TransactionMapperProfile()
        {
            this.CreateMap<Transaction, TransactionResponse>();
        }
    }
}
