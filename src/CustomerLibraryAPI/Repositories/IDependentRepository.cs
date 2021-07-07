using System.Collections.Generic;

namespace CustomerLibraryAPI.Repositories
{
    public interface IDependentRepository<TEntity> : IRepository<TEntity>
    {
        List<TEntity> ReadByCustomerId(int customerId);
        int CountByCustomerId(int customerId);
    }
}
