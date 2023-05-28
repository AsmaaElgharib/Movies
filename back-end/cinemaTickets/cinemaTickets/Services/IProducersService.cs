using cinemaTickets.Communication;
using cinemaTickets.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cinemaTickets.Services
{
    public interface IProducersService
    {
        Task<List<Producer>> GetAll();
        Task<Producer> GetById(int id);
        Task<GeneralResponse<bool>> Persist(Producer producer);
        Task<Producer> Add(Producer producer);
        Task<Producer> Update(int id, Producer newProducer);
        void Delete(int id);
    }
}
