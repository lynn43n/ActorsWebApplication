using ActorsWebApplication.DTO;
using System.Numerics;

namespace ActorsWebApplication.Models
{
    /// <summary>
    /// Represents an actor.
    /// </summary>
    public class ActorModel :ActorBase
    {        
        
        public string Details { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string Source { get; set; }

        public static implicit operator ActorModel(UpsertRequest upsertRequest)
        {
            return new ActorModel()
            {
                Name = upsertRequest.Name,
                Details = upsertRequest.Details,
                Type = upsertRequest.Type,
                Rank = upsertRequest.Rank,
                Source = upsertRequest.Source
            };
        }
    }
}
