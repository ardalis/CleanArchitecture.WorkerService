namespace CleanArchitecture.Core.Settings;

/// <summary>
/// An example settings class used to configure a service
/// </summary>
public class EntryPointSettings
{
  public string ReceivingQueueName { get; set; }
  public string SendingQueueName { get; set; }
}
