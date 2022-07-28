using HiLoGame.Services.Session;

namespace HiLoGame.Services
{
    public interface IServiceModelClient<T, T2> where T2 : class
    {
        Task<T> Create(T2 game);
        Task<T> Get(string id);
        Task<T> Update(T2 game);
    }
}
