using System.Collections.Specialized;
using System.Xml.Serialization;

namespace ActorsWebApplication.DTO
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? TraceId { get; set; }
        public bool IsSuccess { get; set; }
        public Error[] Errors { get; set; }

        public Response(int statusCode, bool isSuccess, Error[] errors = null, string? traceId = null) 
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Errors = errors;
            TraceId = traceId;
        }
    }
}
