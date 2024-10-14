using DoctorAvailabiltity.Repository.Dto;
using Microsoft.AspNetCore.Mvc;
using DoctorAvailabiltity.Services;

namespace DoctorAvailabiltity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTimeAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;

        public DoctorTimeAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorAvailabilityService = doctorAvailabilityService;
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
