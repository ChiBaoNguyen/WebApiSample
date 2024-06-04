using AutoMapper;
using WebAPISample.Entities;
using WebAPISample.Interface.Repository;
using WebAPISample.Interface.Service;
using WebAPISample.Models;

namespace WebAPISample.Service
{
    public class SampleService : ISampleService
    {
        private ISampleRepository sampleRepository;
        private IMapper mapper;
        private readonly ILogger<SampleService> logger;

        public SampleService(ISampleRepository sampleRepository, IMapper mapper, ILogger<SampleService> logger)
        {
            this.sampleRepository = sampleRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<Sample>> GetAllAsync()
        {
            var res = await sampleRepository.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation($" No Sample items found");
            }

            return res;
        }

        public async Task<Sample> GetSampleIdAsync(int id)
        {
            var res = await sampleRepository.GetByIDAsync(id);
            if (res == null)
            {
                logger.LogInformation($"No Sample item with Id {id} found.");
            }

            return res;
        }

        public async Task CreateSampleAsync(CreateSampleRequest request)
        {
            await using (var transaction = sampleRepository.BeginTransaction())
            {
                try
                {
                    // DATA
                    var dataAdd = mapper.Map<Sample>(request);
                    dataAdd.CreatedAt = DateTime.Now;

                    // CREATE & SAVE
                    await sampleRepository.CreateAsync(dataAdd);
                    await sampleRepository.SaveChangeAsync();

                    // COMMIT
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // ROLLBACK
                    await transaction.RollbackAsync();

                    logger.LogError(ex, "An error occurred while creating the todo item.");
                }
            }
        }

        public async Task UpdateSample(int id, UpdateSampleRequest request)
        {
            await using (var transaction = sampleRepository.BeginTransaction())
            {
                try
                {
                    // FINDED
                    var dataTable = await sampleRepository.GetByIDAsync(id);
                    if (dataTable != null)
                    {
                        var dataUpdate = mapper.Map(request, dataTable);
                        dataUpdate.UpdatedAt = DateTime.Now;

                        // UPDATE & SAVE
                        sampleRepository.Update(dataUpdate);
                        await sampleRepository.SaveChangeAsync();

                        // COMMIT
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        logger.LogInformation(" No Sample items found");
                    }
                }
                catch (Exception ex)
                {
                    // ROLLBACK
                    await transaction.RollbackAsync();

                    logger.LogError(ex, "An error occurred while updating the todo item.");
                }
            }
        }

        public async Task DeleteSampleAsync(int id)
        {
            await using (var transaction = sampleRepository.BeginTransaction())
            {
                try
                {
                    // FINDED
                    var data = await sampleRepository.GetByIDAsync(id);
                    if (data != null)
                    {
                        // DELETE & SAVE
                        sampleRepository.Delete(data);
                        await sampleRepository.SaveChangeAsync();

                        // COMMIT
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        logger.LogInformation($"No item found with the id {id}");
                    }
                }
                catch (Exception ex)
                {
                    // ROLLBACK
                    await transaction.RollbackAsync();

                    logger.LogError(ex, "An error occurred while remove the todo item.");
                }
            }
        }
    }
}