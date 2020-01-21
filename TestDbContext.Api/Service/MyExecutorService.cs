using System;
using System.Threading.Tasks;
using TestDbContext.Api.Infra.Executor;
using TestDbContext.Api.Infra.Filler;
using TestDbContext.Api.Infra.Retriever;
using TestDbContext.Domain;

namespace TestDbContext.Api.Service
{
    public class MyExecutorService : IExecutorService
    {
        private const int ChunkSize = 50;
        private readonly IFiller _filler;
        private readonly ITestDataModelRetriever _retriever;
        private readonly IExecutor _executor;

        public MyExecutorService(IFiller filler, ITestDataModelRetriever retriever, IExecutor executor)
        {
            _filler = filler;
            _retriever = retriever;
            _executor = executor;
        }
        public async Task<Result> HandleAllTestDataModelAsync()
        {
            var resultFill = await _filler.FillAsync(100)
                .ConfigureAwait(false);

            if (resultFill.Equals(Result.Error))
            {
                Console.WriteLine("Impossible to fill the database");
                return resultFill;
            }


            int count = 0;
            await foreach (var item in _retriever.FindAll())
            {
                var resultHandle = await _executor.HandleDataModelAsync(item)
                    .ConfigureAwait(false);
                if (resultHandle.Equals(Result.Error))
                {
                    Console.WriteLine("Impossible to handle data model");
                    return resultHandle;
                }

                count++;
                if (count == ChunkSize)
                {
                    var resultPostExecution = await _executor.PostExecutionAsync()
                        .ConfigureAwait(false);
                    if (resultPostExecution.Equals(Result.Error))
                    {
                        Console.WriteLine("Impossible to execute post operation");
                        return resultPostExecution;
                    }

                    count = 0;
                }
            }

            if (count != 0)
            {
                var resultPostExecution = await _executor.PostExecutionAsync()
                    .ConfigureAwait(false);
                return resultPostExecution;
            }

            return Result.Success;
        }
    }
}
