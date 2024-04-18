using ActorsWebApplication.Models;

namespace ActorsWebApplication.DTO
{
    public class ActorResponse : Response
    {
        public ActorModel Actor { get; set; }
        public ActorResponse(int statusCode, bool isSuccess, ActorModel actor, Error[] errors = null, string traceId = null) : base(statusCode, isSuccess, errors, traceId)
        {            
            Actor = actor;
        }
    }
}
