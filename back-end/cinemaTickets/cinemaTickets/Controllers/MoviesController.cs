using cinemaTickets.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using cinemaTickets.Services;
using cinemaTickets.Communication;
using System.Collections.Generic;
using cinemaTickets.Entities;
using cinemaTickets.Constants.Enum;
using Microsoft.Extensions.Logging;

namespace cinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly ILogger<MoviesController> _logger;
        public MoviesController(IMoviesService moviesService, ILogger<MoviesController> logger)
        {
            _moviesService = moviesService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<GeneralResponse<List<Movie>>> GetAllAsync()
        {
            try
            {
                return new GeneralResponse<List<Movie>>()
                {
                    Resource = await _moviesService.GetAll(),
                    Message = "Sucess",
                    Status = EResponseStatus.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
                return new GeneralResponse<List<Movie>>(ex.Message, EResponseStatus.Exception);
            }
        }
        [HttpGet("id")]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _moviesService.GetById(id);
            if (cinemaDetails == null)
                return BadRequest();
            return Ok(cinemaDetails);
        }
        [HttpPost]
        public async Task<GeneralResponse<bool>> Persist(Movie movie)
        {
            try
            {
                await _moviesService.Persist(movie);
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
                return new GeneralResponse<bool>(ex.Message, EResponseStatus.Exception);
            }
        }
        
       
        [HttpDelete]
        public void DeleteAsync(int id)
        {
            try
            {
                _moviesService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " | Trace : " + ex.StackTrace);
            }
        }
    }
}
