using WebAPISample.Entities;
using WebAPISample.Models;

namespace WebAPISample.Interface.Service
{
    public interface ISampleService
    {
        Task<IEnumerable<Sample>> GetAllAsync();
        Task<Sample> GetSampleIdAsync(int id);
        Task CreateSampleAsync(CreateSampleRequest request);
        Task UpdateSample(int id, UpdateSampleRequest request);
        Task DeleteSampleAsync(int id);
    }
}
