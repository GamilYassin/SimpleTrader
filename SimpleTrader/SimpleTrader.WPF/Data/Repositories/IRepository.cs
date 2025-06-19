using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Data.Repositories;

public interface IRepository<T> where T : EntityBase
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task<T> CreateAsync(T entity);

    Task<T?> UpdateAsync(Guid id, T? entity);

    Task<bool> DeleteAsync(Guid id);
}