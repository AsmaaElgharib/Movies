using cinemaTickets.Communication;
using cinemaTickets.Constants.Enum;
using cinemaTickets.Data;
using cinemaTickets.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinemaTickets.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _dbContext;
        public ActorsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GeneralResponse<bool>> Add(Actor actor)
        {
            var newActor = new Actor
            {
                ProfilePictureUrl = actor.ProfilePictureUrl,
                FullName = actor.FullName,
                Bio = actor.Bio
            };
             await _dbContext.Actors.AddAsync(newActor);
             await _dbContext.SaveChangesAsync();
            return new GeneralResponse<bool>("Success", EResponseStatus.Success);

        }

        public void Delete(int id)
        {
            var actor = _dbContext.Actors.FirstOrDefault(x => x.Id == id);
            if (actor != null)
            {
                 _dbContext.Actors.Remove(actor);
                _dbContext.SaveChanges();
            }
        }

        public async  Task<List<Actor>> GetAll() => await _dbContext.Actors.ToListAsync();
        

        public async Task<Actor> GetById(int id) =>
            await _dbContext.Actors.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse<bool>> Persist(Actor actor)
        {
            if(actor.Id > 0)
            {
                var oldactor = await _dbContext.Actors.FirstOrDefaultAsync(x => x.Id == actor.Id);
                if(oldactor != null)
                {
                    oldactor.ProfilePictureUrl = actor.ProfilePictureUrl;
                    oldactor.FullName = actor.FullName;
                    oldactor.Bio = actor.Bio;
                    await _dbContext.SaveChangesAsync();
                }
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            else
            {
                await _dbContext.Actors.AddAsync(actor);
                await _dbContext.SaveChangesAsync();
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
        }

        public async Task<GeneralResponse<bool>> Update(int id, Actor newactor)
        {
            var actor = await _dbContext.Actors.FirstOrDefaultAsync(x => x.Id == id);
            
            actor.ProfilePictureUrl = newactor.ProfilePictureUrl;
            actor.FullName = newactor.FullName;
            actor.Bio = newactor.Bio;
            await _dbContext.SaveChangesAsync();
            return new GeneralResponse<bool>("Success", EResponseStatus.Success);
        }
    }
}
