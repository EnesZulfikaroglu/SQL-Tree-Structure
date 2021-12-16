﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}