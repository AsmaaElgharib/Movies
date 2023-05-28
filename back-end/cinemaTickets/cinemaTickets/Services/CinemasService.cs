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
    public class CinemasService : ICinemasService
    {
        private readonly AppDbContext _dbContext;
        public CinemasService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cinema> Add(Cinema cinema)
        {
            var newCinema = new Cinema
            {
                Name = cinema.Name,
                Description = cinema.Description,
                Logo = cinema.Logo
            };
            await _dbContext.Cinemas.AddAsync(newCinema);
            await _dbContext.SaveChangesAsync();
            return newCinema;
        }

        public void Delete(int id)
        {
            var cinema = _dbContext.Cinemas.FirstOrDefault(x => x.Id == id);
            if (cinema != null)
            {
                _dbContext.Cinemas.Remove(cinema);
                _dbContext.SaveChanges();
            }
        }
        public async Task<GeneralResponse<bool>> Persist(Cinema cinema)
        {
            if (cinema.Id > 0)
            {
                var oldacinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == cinema.Id);
                if (oldacinema != null)
                {
                    oldacinema.Name = cinema.Name;
                    oldacinema.Logo = cinema.Logo;
                    oldacinema.Description = cinema.Description;
                    await _dbContext.SaveChangesAsync();
                }
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            else
            {
                await _dbContext.Cinemas.AddAsync(cinema);
                await _dbContext.SaveChangesAsync();
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
        }
        public async Task<List<Cinema>> GetAll() => await _dbContext.Cinemas.ToListAsync();

        public async Task<Cinema> GetById(int id) =>
            await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Cinema> Update(int id, Cinema newCinema)
        {
            var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
            if (cinema == null)
            {
                return null;
            }
            else
            {
                cinema.Name = newCinema.Name;
                cinema.Logo = newCinema.Logo;
                cinema.Description = newCinema.Description;
                _dbContext.SaveChanges();
                return cinema;
            }
        }
    }
}
