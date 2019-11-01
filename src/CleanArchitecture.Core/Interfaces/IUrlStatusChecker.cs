using CleanArchitecture.Core.Entities;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IUrlStatusChecker
    {
        Task<UrlStatusHistory> CheckUrlAsync(string url, string requestId);
    }
}
