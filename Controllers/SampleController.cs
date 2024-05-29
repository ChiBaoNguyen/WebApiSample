using Microsoft.AspNetCore.Mvc;
using WebAPISample.Interface.Service;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController : Controller
    {
        private readonly ISampleService sampleService;

        public SampleController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var Sample = await sampleService.GetAllAsync();

                if (Sample == null || !Sample.Any())
                {
                    return Ok(new { message = "No Sample Items found" });
                }

                return Ok(new { message = "Successfully retrieved all sample ", data = Sample });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all sample it", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSampleIdAsync(int id)
        {
            try
            {
                var Sample = await sampleService.GetSampleIdAsync(id);
                if (Sample == null)
                {
                    return NotFound(new { message = $"Sample Item  with id {id} not found" });
                }

                return Ok(new { message = "Successfully retrieved all sample ", data = Sample });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all sample it", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSampleAsync(CreateSampleRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await sampleService.CreateSampleAsync(request);
                return Ok(new { message = "Sample successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the crating Sample Item", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSampleAsync(int id, UpdateSampleRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var Sample = await sampleService.GetSampleIdAsync(id);
                if (Sample == null)
                {
                    return NotFound(new { message = $"Sample Item  with id {id} not found" });
                }

                await sampleService.UpdateSample(id, request);
                return Ok(new { message = $" Sample Item  with id {id} successfully updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating sample with id {id}", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSampleAsync(int id)
        {
            try
            {
                var Sample = await sampleService.GetSampleIdAsync(id);
                if (Sample == null)
                {
                    return NotFound(new { message = $"Sample Item  with id {id} not found" });
                }

                await sampleService.DeleteSampleAsync(id);
                return Ok(new { message = $"Sample  with id {id} successfully deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Sample Item  with id {id}", error = ex.Message });
            }
        }
    }
}
