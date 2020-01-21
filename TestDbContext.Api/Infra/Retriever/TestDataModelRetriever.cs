using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestDbContext.Domain.DataModel;
using TestDbContext.Infra.Db;

namespace TestDbContext.Api.Infra.Retriever
{
    public class TestDataModelRetriever : ITestDataModelRetriever
    {
        private readonly MyDbContext _dbContext;

        public TestDataModelRetriever(MyDbContext dbContex)
        {
            _dbContext = dbContex;
        }

        public IAsyncEnumerable<TestDataModel> FindAll()
        {
            return _dbContext.DataModels.AsAsyncEnumerable();
        }
    }
}
