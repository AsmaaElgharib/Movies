using cinemaTickets.Communication;
using cinemaTickets.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cinemaTickets.Services
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetAll();
        Task<Movie> GetById(int id);
        Task<GeneralResponse<bool>> Persist(Movie movie);
        void Delete(int id);
    }
}
