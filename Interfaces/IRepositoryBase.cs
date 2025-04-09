using System.Linq.Expressions;

namespace markdown_note_taking_app.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,bool trackChanges);
        void Create(T entitiy);
        void Update(T entity);
        void Delete(T entity);
    }
}
