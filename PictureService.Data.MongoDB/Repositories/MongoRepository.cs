using MongoDB.Bson;
using MongoDB.Driver;
using PictureService.Domain.Attributes;
using PictureService.Domain.Attributes.Interface;
using PictureService.Domain.Repositories.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PictureService.Data.MongoDB.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public async Task<List<TDocument>> GetAll()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
        public MongoRepository(IMongoDbSettings settings)
        {
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentException("MongoDB connection string is null or empty.");
            }

            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> expression)
        {
            return _collection.Find(expression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(expression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> expression)
        {
            return _collection.Find(expression).FirstOrDefault();
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> expression)
        {
            return await _collection.Find(expression).FirstOrDefaultAsync();
        }

        public virtual TDocument FindById(Guid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual async Task<TDocument> FindByIdAsync(Guid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }


        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual async Task InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteById(Guid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        private protected string? GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public async Task<bool> Exists(Guid id)
        {

            throw new NotImplementedException();
        }

        public void DeleteManyByIds(IEnumerable<Guid> ids)
        {
            var filter = Builders<TDocument>.Filter.In(doc => doc.Id, ids);
            _collection.DeleteMany(filter);
        }

        public async Task DeleteManyByIdsAsync(IEnumerable<Guid> ids)
        {
            var filter = Builders<TDocument>.Filter.In(doc => doc.Id, ids);
            await _collection.DeleteManyAsync(filter);
        }
    }
}
