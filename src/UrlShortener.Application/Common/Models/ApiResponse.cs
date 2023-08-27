namespace UrlShortener.Application.Common.Models;

public class ApiResponse
{
    public object Result { get; set; }

    public static ApiResponse Create(object result)
    {
        return new ApiResponse(result);
    }

    protected ApiResponse(object result)
    {
        Result = result;
    }
}