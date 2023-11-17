using Ardalis.GuardClauses;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services;

/// <summary>
/// A simple service that fetches a URL and returns a UrlStatusHistory instance with the result
/// </summary>
public class UrlStatusChecker(IHttpService _httpService) : IUrlStatusChecker
{
  public async Task<UrlStatusHistory> CheckUrlAsync(string url, string requestId)
  {
    Guard.Against.NullOrWhiteSpace(url, nameof(url));

    var statusCode = await _httpService.GetUrlResponseStatusCodeAsync(url);

    return new UrlStatusHistory
    {
      RequestId = requestId,
      StatusCode = statusCode,
      Uri = url
    };
  }
}
