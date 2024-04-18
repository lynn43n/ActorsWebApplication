using ActorsWebApplication.DTO;
using ActorsWebApplication.Models;

namespace ActorsWebApplication.Services
{
    public interface IActorsService
    {
        public Task<IEnumerable<ActorModel>> GetActors(
           string actorName = null,
           int? minRank = null,
           int? maxRank = null,
           string provider = null,
           int skip = 0,
           int take = 20);
        public Task<ActorModel> Get(string id);
        public Task<ActorModel> Add(ActorModel actor);
        public Task<ActorModel> Delete(string id);
        public Task<ActorModel> Put(ActorModel actor);


    }
}
