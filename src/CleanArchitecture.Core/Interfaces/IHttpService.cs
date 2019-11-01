using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IHttpService
    {
        Task<int> GetUrlResponseStatusCodeAsync(string url);
    }
}
