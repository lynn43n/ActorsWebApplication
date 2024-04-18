using ActorsWebApplication.Models;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace ActorsWebApplication.DTO
{
    public class ActorsResponse : Response
    {
        public ActorsResponse(int statusCode, bool isSuccess, IEnumerable<ActorBase> actors, Error[] errors = null, string traceId = null) : base(statusCode, isSuccess, errors, traceId)
        {
            Actors = actors;
        }

        public IEnumerable<ActorBase> Actors { get; set; }

    }
}
