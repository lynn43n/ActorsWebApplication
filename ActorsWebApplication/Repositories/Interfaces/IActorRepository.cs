using ActorsWebApplication.DTO;
using ActorsWebApplication.Models;

namespace ActorsWebApplication.Data.Interfaces
{
    public interface IActorRepository
    {
        public Task<IEnumerable<ActorModel>> GetActors(
            string actorName = null,
            int? minRank = null,
            int? maxRank = null,
            string provider = null,
            int skip = 0,
            int take = 20);
        public Task<ActorModel> Get(string id);
        public ActorModel Add(ActorModel actor);
        public ActorModel Delete(ActorModel actor);
        public void Put(ActorModel actor);
    }
}
