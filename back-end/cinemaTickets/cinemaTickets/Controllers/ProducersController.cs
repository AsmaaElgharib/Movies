using cinemaTickets.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using cinemaTickets.Services;
using cinemaTickets.Entities;
using cinemaTickets.Communication;
using System.Collections.Generic;
using cinemaTickets.Constants.Enum;

namespace cinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducersService _producersService;
        public ProducersController(IProducersService producersService)
        {
            _producersService = producersService;
        }
        [HttpGet]
        public async Task<GeneralResponse<List<Producer>>> GetAllAsync()
        {
            try
            {
                return new GeneralResponse<List<Producer>>()
                {
                    Resource = await _producersService.GetAll(),
                    Message = "Sucess",
                    Status = EResponseStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<Producer>>(ex.Message, EResponseStatus.Exception);

            }
        }
        [HttpPost]
        public async Task<GeneralResponse<bool>> Persist(Producer producer)
        {
            try
            {
                await _producersService.Persist(producer);
                return new GeneralResponse<bool>("Success", EResponseStatus.Success);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<bool>(ex.Message, EResponseStatus.Exception);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _producersService.GetById(id);
            if (producerDetails == null)
                return BadRequest();
            return Ok(producerDetails);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateAsync(Producer producer)
        //{
        //    try
        //    {
        //        return Ok(await _producersService.Add(producer));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, Producer producer)
        {
            try
            {
                return Ok(await _producersService.Update(id, producer));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public void DeleteAsync(int id)
        {
            try
            {
                _producersService.Delete(id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
