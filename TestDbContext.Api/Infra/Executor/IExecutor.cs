using System.Threading.Tasks;
using TestDbContext.Domain;
using TestDbContext.Domain.DataModel;

namespace TestDbContext.Api.Infra.Executor
{
    public interface IExecutor
    {
         public Task<Result> HandleDataModelAsync(TestDataModel dataModel);
         public Task<Result> PostExecutionAsync();
    }
}
