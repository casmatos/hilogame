using System.Linq.Expressions;

namespace HILoGame.WebApi.Services
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllByFilter(Expression<Func<T, bool>> filter);
        Task<T> GetById(string id);
        Task<T> GetByFilter(Expression<Func<T, bool>> filter);
        Task<T> Create(T createRcord);
        Task Update(string id, T updateRecord);
        Task<bool> Remove(string id);
    }
}