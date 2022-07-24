namespace DentallApp.Helpers;

public class ResponseBase
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public ResponseBase()
    {

    }

    public ResponseBase(string message)
    {
        Message = message;
    }
}
