namespace SupCountBE.Core.Repositories;

public interface IExpenseRepository : IAsyncRepository<Expense>
{
    Task<Expense?> GetByIdIncludingAsync(
        int id,
        bool includePayer = false,
        bool includeCategory = false,
        bool includeGroup = false,
        bool includeParticipations = false,
        bool includeJustifications = false
    );
}
