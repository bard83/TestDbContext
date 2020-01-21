using System.Threading.Tasks;
using TestDbContext.Domain;

namespace TestDbContext.Api.Infra.Filler
{
    public interface IFiller
    {
         public Task<Result> FillAsync(int size);
    }
}
