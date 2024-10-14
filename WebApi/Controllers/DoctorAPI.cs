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
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;

        public DoctorController(IDoctorServices doctorService, IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorService = doctorService;
            _doctorAvailabilityService = doctorAvailabilityService;
        }

        /// <summary>
        /// Inserts A new doctor with hos availabilities as I provide id of the day,
        /// and the time range if existed then reference if not then add it and reference.
        /// </summary>
        /// <param name="doctorDto">contains the name of the doctor and the avaialabilities ids.</param>
        /// <returns>Successful if yes and notfound if day doesn't exist</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto doctorDto)
        {
            if (doctorDto == null || string.IsNullOrEmpty(doctorDto.DoctorName))
            {
                return BadRequest("Invalid doctor data.");
            }
            await _doctorService.AddDoctorAsync(doctorDto);

            return Ok(new { Message = "Doctor created successfully!", doctorDto.DoctorName });
        }
        /// <summary>
        /// Return the required doctor by his id.
        /// </summary>
        /// <param name="id">id of the doctor</param>
        /// <returns>return Single doctor with his availabilities.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctorDto = await _doctorService.GetDoctorByIdAsync(id);
            if (doctorDto == null)
                return NotFound();

            return Ok(doctorDto);
        }

        /// <summary>
        /// update the availability of the doctor.
        /// </summary>
        /// <param name="doctorId">Doctor id</param>
        /// <param name="updateDto">Mainly it contains the id of the day
        /// and the time ranges</param>
        /// <returns>Success message if change successful and not found if not existed </returns>
        [HttpPut("{doctorId}/availability")]
        public async Task<IActionResult> UpdateDoctorAvailability(int doctorId, [FromBody] UpdateDoctorTimeAvailabilityDto updateDto)
        {
            try
            {
                await _doctorAvailabilityService.UpdateDoctorAvailabilityAsync(doctorId, updateDto);
                return Ok(new { message = "Doctor availability updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
