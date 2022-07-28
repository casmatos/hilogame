using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;
using System.Linq.Expressions;

namespace HILoGame.WebApi.Services
{
    public interface IBasePlayerService : IBaseService<PlayerDTO>
    {
        Task<IEnumerable<PlayerDTO>> GetPlayersByFilter(Expression<Func<Player, bool>> filter);
        Task IncreseInteractions(PlayerDTO player);
    }
}
