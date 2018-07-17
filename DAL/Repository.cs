﻿using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AirportRESRfulApi.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly AirportContext context;

        public Repository(AirportContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            var entity = query.SingleOrDefault(x => x.Id == id);

            return entity;
        }

        public virtual void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Create(IEnumerable<TEntity> entitys)
        {
            context.Set<TEntity>().AddRange(entitys);
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
    }
}
