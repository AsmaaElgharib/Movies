using cinemaTickets.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using cinemaTickets.Services;
using cinemaTickets.Entities;
using cinemaTickets.Communication;
using System.Collections.Generic;
using cinemaTickets.Constants.Enum;

namespace cinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly ICinemasService _cinemasService;
        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }
        [HttpGet]
        public async Task<GeneralResponse<List<Cinema>>> GetAllAsync()
        {
            try
            {
                return new GeneralResponse<List<Cinema>>()
                {
                    Resource = await _cinemasService.GetAll(),
                    Message = "Sucess",
                    Status = EResponseStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<Cinema>>(ex.Message, EResponseStatus.Exception);
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateAsync(Cinema cinema)
        //{
        //    try
        //    {
        //        return Ok(await _cinemasService.Add(cinema));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}
        [HttpGet("id")]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _cinemasService.GetById(id);
            if (cinemaDetails == null)
                return BadRequest();
            return Ok(cinemaDetails);
        }
        [HttpPost]
        public async Task<GeneralResponse<bool>> Persist(Cinema cinema)
        {
            try
            {
                await _cinemasService.Persist(cinema);
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<bool>(ex.Message, EResponseStatus.Exception);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, Cinema cinema)
        {
            try
            {
                return Ok(await _cinemasService.Update(id, cinema));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("id")]
        public void DeleteAsync(int id)
        {
            try
            {
                _cinemasService.Delete(id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
