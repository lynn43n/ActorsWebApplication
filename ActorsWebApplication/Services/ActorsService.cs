using ActorsWebApplication.Data;
using ActorsWebApplication.Data.Interfaces;
using ActorsWebApplication.DTO;
using ActorsWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ActorsWebApplication.Services;

public class ActorsService : IActorsService
{
    private readonly IActorRepository _actorRepo;

    public ActorsService(IActorRepository actorRepo)
    {
        _actorRepo = actorRepo;
    }
    public async Task<IEnumerable<ActorModel>> GetActors(
           string actorName = null,
           int? minRank = null,
           int? maxRank = null,
           string provider = null,
           int skip = 0,
           int take = 20)
    {
        return await _actorRepo.GetActors(actorName, minRank, maxRank, provider, skip, take);
     
    }

    public async Task<ActorModel> Get(string id)
    {
        return await _actorRepo.Get(id);        
    }

    public async Task<ActorModel> Add(ActorModel actor)
    {
        return _actorRepo.Add(actor);
    }

    public async Task<ActorModel> Delete(string id)
    {

        var actor = await _actorRepo.Get(id);
        _actorRepo.Delete(actor);
        return actor;
    }

    public async Task<ActorModel> Put(ActorModel actor)
    {
        var existingActor = await _actorRepo.Get(actor.Id);
        existingActor.Name = actor.Name;
        existingActor.Rank = actor.Rank;
        existingActor.Source = actor.Source;
        existingActor.Type = actor.Type;
        existingActor.Details = actor.Details;

        _actorRepo.Put(existingActor);
        return existingActor;
    }
}
    

