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
    public class MoviesService : IMoviesService
    {
        private readonly AppDbContext _dbContext;
        public MoviesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Movie>> GetAll()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<GeneralResponse<bool>> Persist(Movie movie)
        {
            if (movie.Id > 0)
            {
                var oldMovie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == movie.Id);
                if (oldMovie != null)
                {
                    oldMovie.Name = movie.Name;
                    oldMovie.Price = movie.Price;
                    oldMovie.StartDate= movie.StartDate;
                    oldMovie.EndDate = movie.EndDate;
                    oldMovie.Description = movie.Description;
                    oldMovie.CinemaId = movie.CinemaId;
                    oldMovie.ImageUrl = movie.ImageUrl;
                    oldMovie.ProducerId = movie.ProducerId;
                    oldMovie.MovieCategory = movie.MovieCategory;
                    oldMovie.Actors_Movies = movie.Actors_Movies;
                    await _dbContext.SaveChangesAsync();
                }
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            else
            {
                await _dbContext.Movies.AddAsync(movie);
                await _dbContext.SaveChangesAsync();
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
        }
        public void Delete(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();
            }
        }
    }
}
