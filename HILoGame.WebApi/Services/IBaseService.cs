using System.Linq.Expressions;

namespace HILoGame.WebApi.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task<T> Create(T createRcord);
        Task Update(T updateRecord);
        Task<bool> Remove(string id);
    }
}
