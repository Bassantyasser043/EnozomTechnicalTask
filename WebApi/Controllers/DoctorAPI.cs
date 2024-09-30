using DoctorAvailabiltity.Repository.Dto;
using DoctorAvailabiltity.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAvailabiltity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorService;

        public DoctorController(IDoctorServices doctorService)
        {
            _doctorService = doctorService;
        }
        /// <summary>
        /// Inserts A new doctor with hos availabilities as I provide id of the day,
        /// and the time range if existed then reference if not then add it and reference.
        /// </summary>
        /// <param name="doctorDto">The source station name.</param>
        /// <returns>It doesn't return anything.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto doctorDto)
        {
            if (doctorDto == null || string.IsNullOrEmpty(doctorDto.DoctorName))
            {
                return BadRequest("Invalid doctor data.");
            }
            await _doctorService.InsertDoctor(doctorDto);

            return Ok(new { Message = "Doctor created successfully!", doctorDto.DoctorName });
        }
        /// <summary>
        /// Return the required doctor by his id.
        /// </summary>
        /// <param name="id">The source station name.</param>
        /// <returns>return Single doctor with his availabilities.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctorDto = await _doctorService.GetDoctorByIdAsync(id);
            if (doctorDto == null)
                return NotFound();

            return Ok(doctorDto);
        }

    }
}
