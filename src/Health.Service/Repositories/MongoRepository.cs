using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Health.Service.Entities;

namespace Health.Service.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(x => x.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            await dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            await dbCollection.ReplaceOneAsync(filterBuilder.Eq(x => x.Id, item.Id), item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await dbCollection.DeleteOneAsync(filterBuilder.Eq(x => x.Id, id));
        }
    }
}