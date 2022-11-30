using System;

namespace CleanArchitecture.Core.Entities;

/// <summary>
/// Tracks the status attempts to periodically GET a URL 
/// </summary>
public class UrlStatusHistory : BaseEntity
{
  public string Uri { get; set; }
  public DateTime RequestDateUtc { get; } = DateTime.UtcNow;
  public int StatusCode { get; set; }
  public string RequestId { get; set; }

  public override string ToString()
  {
    return $"Fetched {Uri} at {RequestDateUtc.ToLocalTime()} with status code {StatusCode}.";
  }
}
