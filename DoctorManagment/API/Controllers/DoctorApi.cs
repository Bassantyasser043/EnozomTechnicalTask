using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Dto;
using Services;

namespace API.Controllers
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
        /// <param name="doctorDto">contains the name of the doctor and the avaialabilities ids.</param>
        /// <returns>Successful if yes and notfound if day doesn't exist</returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDto doctorDto)
        {
            if (doctorDto == null || string.IsNullOrEmpty(doctorDto.DoctorName))
            {
                return BadRequest("Invalid doctor data.");
            }
            try
            {
                var createdDoctor = await _doctorService.AddDoctorAsync(doctorDto);

                return CreatedAtAction(nameof(GetDoctorById), new { id = createdDoctor.DoctorId },
                           new { Message = "Doctor created successfully!", doctorDto.DoctorName });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "An error occurred while creating the doctor. Please try again.");
            }
        }
        /// <summary>
        /// Return the required doctor by his id.
        /// </summary>
        /// <param name="id">id of the doctor</param>
        /// <returns>return Single doctor with his availabilities.</returns>
        /// 
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
