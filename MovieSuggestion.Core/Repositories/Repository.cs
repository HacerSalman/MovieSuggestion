using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Contexts;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MovieDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public Repository(MovieDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            OnBeforeCreate(entity as BaseEntity);
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                OnBeforeCreate(entity as BaseEntity);
            }
          
            await context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public ValueTask<TEntity> GetByIdAsync(ulong id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public bool Add(TEntity entity)
        {
            OnBeforeCreate(entity as BaseEntity);
            context.Set<TEntity>().Add(entity);
            return true;
        }

        public async Task<bool> Update(TEntity entity)
        {
            OnBeforeUpdate(entity as BaseEntity);
            context.Set<TEntity>().Update(entity);
            return await Task.FromResult(true);
        }
        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        private string GetCurrentUsername()
        {
            string currentUsername = "anonymous";
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                var nameIdentifier = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (nameIdentifier != null && !string.IsNullOrEmpty(nameIdentifier.Value))
                {
                    currentUsername = nameIdentifier.Value;
                }

            }
            return currentUsername;
        }
        private void OnBeforeUpdate(BaseEntity entity) 
        {
            entity.UpdateTime = DateTimeUtils.GetCurrentTicks();
            entity.Modifier = GetCurrentUsername();                 
        }

        private void OnBeforeCreate(BaseEntity entity)
        {
            entity.CreateTime = DateTimeUtils.GetCurrentTicks();
            entity.Owner = GetCurrentUsername();
            entity.UpdateTime = DateTimeUtils.GetCurrentTicks();
            entity.Modifier = GetCurrentUsername();
        }
    }
}
