using cinemaTickets.Communication;
using cinemaTickets.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cinemaTickets.Services
{
    public interface ICinemasService
    {
        Task<List<Cinema>> GetAll();
        Task<Cinema> GetById(int id);
        Task<GeneralResponse<bool>> Persist(Cinema cinema);
        Task<Cinema> Add(Cinema cinema);
        Task<Cinema> Update(int id, Cinema newCinema);
        void Delete(int id);
    }
}
