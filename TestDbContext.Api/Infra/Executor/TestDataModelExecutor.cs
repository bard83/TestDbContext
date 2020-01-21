using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDbContext.Domain;
using TestDbContext.Domain.DataModel;
using TestDbContext.Infra.Db;

namespace TestDbContext.Api.Infra.Executor
{
    public class TestDataModelExecutor : IExecutor
    {
        private readonly MyDbContext _context;

        public TestDataModelExecutor(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Result> HandleDataModelAsync(TestDataModel dataModel)
        {
            try
            {
                // data model already in the context
                var local = _context.Set<TestDataModel>()
                    .Local
                    .FirstOrDefault(r => r.Name == dataModel.Name && r.Timestamp.CompareTo(dataModel.Timestamp) == 0);
                if (local == default(TestDataModel))
                {
                    local = await _context.DataModels!
                        .FirstOrDefaultAsync(r => r.Name == dataModel.Name && r.Timestamp.CompareTo(dataModel.Timestamp) == 0);
                    if (local == default(TestDataModel))
                    {
                        throw new InvalidOperationException("Test data model not found");
                    }
                }

                local.Property = DateTimeOffset.Now.ToString("o");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error occurred during handling data model {0} - {1}", e.Message, e.StackTrace);
                return Result.Error;
            }

            return Result.Success;
        }

        public async Task<Result> PostExecutionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error occurred during saving data model {0} - {1}", e.Message, e.StackTrace);
                return Result.Error;
            }

            return Result.Success;
        }
    }
}
