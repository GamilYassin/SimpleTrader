using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.Entities;
using FieldOps.Kernel.Functional;

namespace SimpleTrader.WPF.Data.Repositories;

public interface IRepository<T> where T : EntityBase
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<Validation<T>> GetByIdAsync(Guid id);

    Task<Validation<T>> CreateAsync(T entity);

    Task<Validation<T>> UpdateAsync(Guid id, T? entity);

    Task<Validation<bool>> DeleteAsync(Guid id);
}