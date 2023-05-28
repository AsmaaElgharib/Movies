using cinemaTickets.Communication;
using cinemaTickets.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cinemaTickets.Services
{
    public interface IActorsService
    {
        Task<List<Actor>> GetAll();
        Task<Actor> GetById(int id);
        Task<GeneralResponse<bool>> Add(Actor actor);
        Task<GeneralResponse<bool>> Update(int id,  Actor newactor);
        Task<GeneralResponse<bool>> Persist(Actor actor);
        void Delete(int id);
    }
}
