using TestDbContext.Domain.DataModel;
using System.Collections.Generic;

namespace TestDbContext.Api.Infra.Retriever
{
    public interface ITestDataModelRetriever
    {
         public IAsyncEnumerable<TestDataModel> FindAll();
    }
}
