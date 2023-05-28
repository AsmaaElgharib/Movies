using cinemaTickets.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using cinemaTickets.Services;
using cinemaTickets.Entities;
using cinemaTickets.Communication;
using cinemaTickets.Constants.Enum;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace cinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsService _actorsService;
        private readonly ILogger<MoviesController> _logger;
        public ActorsController(IActorsService actorsService, ILogger<MoviesController> logger)
        {
            _actorsService = actorsService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<GeneralResponse<List<Actor>>> GetAllAsync()
        {
            try
            {
                return new GeneralResponse<List<Actor>>()
                {
                    Resource = await _actorsService.GetAll(),
                    Message = "Sucess",
                    Status = EResponseStatus.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
                return new GeneralResponse<List<Actor>>(ex.Message, EResponseStatus.Exception);
            }
        }
        [HttpPost]
        public async Task<GeneralResponse<bool>> Persist(Actor actor)
        {
            try
            {
                await _actorsService.Persist(actor);
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
                return new GeneralResponse<bool>(ex.Message, EResponseStatus.Exception);
            }
        }
        //[HttpPost]
        //public async Task<GeneralResponse<bool>> CreateAsync(Actor actor)
        //{
        //    try
        //    {
        //        await _actorsService.Add(actor);
        //        return new GeneralResponse<bool>("Success", EResponseStatus.Success);
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
        //        return new GeneralResponse<bool>(ex.Message, EResponseStatus.Exception);
        //    }
        //}
        [HttpGet("id")]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _actorsService.GetById(id);
            if (actorDetails == null)
                return BadRequest(); 
            return Ok(actorDetails);
        }
       [HttpPut]
       public async Task<GeneralResponse<bool>> UpdateAsync(int id,Actor actor)
       {
           try
           {
               await _actorsService.Update(id, actor);
               return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            catch (Exception ex)
           {
                return new GeneralResponse<bool>(ex.Message, EResponseStatus.Exception);
           }
        }
        [HttpDelete]
        public void DeleteAsync(int id)
        {
            try
            {
               _actorsService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
            }
        }
    }
}
