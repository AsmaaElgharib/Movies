using cinemaTickets.Constants.Enum;

namespace cinemaTickets.Communication
{
    public class GeneralResponse<T>
    {
        public string Message { get; set; }
        public EResponseStatus Status { get; set; }
        public T Resource { get; set; }
        public GeneralResponse()  {}
        public GeneralResponse(T resource, EResponseStatus status = EResponseStatus.Success)
        {
            Message = string.Empty;
            Resource = resource;    
            Status = status;
        }
        public GeneralResponse(string message, EResponseStatus statusCode)
        {
            Message = message;
            Resource = default;
            Status = statusCode;
        }
    }
}
