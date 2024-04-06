using PictureService.Domain.Attributes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Domain.Repositories.Pictures
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<List<TDocument>> GetAll();

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> expression);
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> expression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> expression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> expression);

        TDocument FindById(Guid id);

        Task<TDocument> FindByIdAsync(Guid id);

        void InsertOne(TDocument document);

        Task InsertOneAsync(TDocument document);

        void InsertMany(ICollection<TDocument> documents);

        Task InsertManyAsync(ICollection<TDocument> documents);

        void ReplaceOne(TDocument document);

        Task ReplaceOneAsync(TDocument document);

        void DeleteById(Guid id);

        Task DeleteByIdAsync(Guid id);

        void DeleteManyByIds(IEnumerable<Guid> ids);

        Task DeleteManyByIdsAsync(IEnumerable<Guid> ids);
        Task<bool> Exists(Guid id);
    }
}
