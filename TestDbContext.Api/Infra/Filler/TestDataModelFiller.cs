using System;
using System.Threading.Tasks;
using TestDbContext.Domain;
using TestDbContext.Domain.DataModel;
using TestDbContext.Infra.Db;

namespace TestDbContext.Api.Infra.Filler
{
    public class TestDataModelFiller : IFiller
    {
        private readonly MyDbContext _context;

        public TestDataModelFiller(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Result> FillAsync(int size)
        {
            for (int i = 0; i < size; i++)
            {
                var name = $"{i}-{DateTime.Now.ToString("YYYY-MM-DD")}";
                var now = DateTimeOffset.Now;
                var dataModel = new TestDataModel(name, now.ToString("o"), now);
                await _context.AddAsync(dataModel);
            }

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
