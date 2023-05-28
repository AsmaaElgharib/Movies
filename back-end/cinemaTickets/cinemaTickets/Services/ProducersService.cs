using cinemaTickets.Communication;
using cinemaTickets.Constants.Enum;
using cinemaTickets.Data;
using cinemaTickets.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinemaTickets.Services
{
    public class ProducersService : IProducersService
    {
        private readonly AppDbContext _dbContext;
        public ProducersService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Producer> Add(Producer producer)
        {
            var newProducer = new Producer
            {
                ProfilePictureUrl = producer.ProfilePictureUrl,
                FullName = producer.FullName,
                Bio = producer.Bio
            };
            await _dbContext.Producers.AddAsync(newProducer);
            await _dbContext.SaveChangesAsync();
            return newProducer;
        }

        public void Delete(int id)
        {
            var producer = _dbContext.Producers.FirstOrDefault(x => x.Id == id);
            if (producer != null)
            {
                _dbContext.Producers.Remove(producer);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<Producer>> GetAll() => await _dbContext.Producers.ToListAsync();

        public async Task<GeneralResponse<bool>> Persist(Producer producer)
        {
            if (producer.Id > 0)
            {
                var oldProducer = await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == producer.Id);
                if (oldProducer != null)
                {
                    oldProducer.ProfilePictureUrl = producer.ProfilePictureUrl;
                    oldProducer.FullName = producer.FullName;
                    oldProducer.Bio = producer.Bio;
                    await _dbContext.SaveChangesAsync();
                }
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            else
            {
                await _dbContext.Producers.AddAsync(producer);
                await _dbContext.SaveChangesAsync();
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
        }

        public async Task<Producer> GetById(int id) =>
            await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == id);
        

        public async Task<Producer> Update(int id, Producer newProducer)
        {
            var producer = await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == id);
            if (producer == null)
            {
                return null;
            }
            else
            {
                producer.ProfilePictureUrl = newProducer.ProfilePictureUrl;
                producer.FullName = newProducer.FullName;
                producer.Bio = newProducer.Bio;
                _dbContext.SaveChanges();
                return producer;
            }
        }
    }
}
