namespace DentallApp.Responses;

public class Response<T> : ResponseBase
{
    public T Data { get; set; }

    public Response()
    {
        
    }

    public Response(string message) : base(message)
    {

    }
}
