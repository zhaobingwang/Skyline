using Skyline.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>, IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
    }
}
