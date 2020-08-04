using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Data.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
