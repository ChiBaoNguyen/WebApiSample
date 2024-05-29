using WebAPISample.EF;
using WebAPISample.Entities;
using WebAPISample.Infrastructure;
using WebAPISample.Interface.Repository;

namespace WebAPISample.Repository
{
    public class SampleRepository : Repository<Sample>, ISampleRepository
    {
        public SampleRepository(SampleDbContext context) : base(context)
        {
        }
    }
}
