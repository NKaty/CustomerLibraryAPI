using System.Collections.Generic;

namespace CustomerLibraryAPI.Repositories
{
    public interface IMainRepository<TEntity> : IRepository<TEntity>
    {
       (List<TEntity>, int) ReadPage(int offset, int limit);

        int Count();
    }
}
