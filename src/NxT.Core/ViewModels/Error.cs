namespace NxT.Core.ViewModels;

public class Error
{
    public string? RequestId { get; set; }
    public string? Message { get; set; }

    public bool ShowRequestId 
        => !string.IsNullOrEmpty(RequestId);
    public bool ShowMessage 
        => !string.IsNullOrEmpty(Message);
}
