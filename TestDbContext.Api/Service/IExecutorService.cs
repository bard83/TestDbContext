using System.Threading.Tasks;
using TestDbContext.Domain;

namespace TestDbContext.Api.Service
{
    public interface IExecutorService
    {
         public Task<Result> HandleAllTestDataModelAsync();
    }
}
