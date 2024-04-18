using ActorsWebApplication.Data.Interfaces;
using ActorsWebApplication.DataProvidors;
using ActorsWebApplication.DTO;
using ActorsWebApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using System.Collections.Specialized;

namespace ActorsWebApplication.Data
{
    public class ActorRepository : IActorRepository
    {
        private readonly ApiContext _context;
        private readonly DbSet<ActorModel> _actors;

        public ActorRepository(ApiContext context)
        {
            _context = context;
            _actors = context.Actors;
        }

        public async Task<IEnumerable<ActorModel>> GetActors(
            string actorName = null,
            int? minRank = null,
            int? maxRank = null,
            string provider = null,
            int skip = 0,
            int take = 20)
        {
            var query = _actors.AsQueryable();

            if (!string.IsNullOrEmpty(actorName))
            {
                query = query.Where(a => a.Name.Contains(actorName));
            }

            if (minRank.HasValue)
            {
                query = query.Where(a => a.Rank >= minRank);
            }

            if (maxRank.HasValue)
            {
                query = query.Where(a => a.Rank <= maxRank);
            }

            return query
                .Skip(skip * take)
                .Take(take)
                .Select(a => new ActorModel { Id = a.Id, Name = a.Name });
            
        }

        public async Task<ActorModel> Get(string id)
        {
            ActorModel actor = await _actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
            {
                throw new KeyNotFoundException("Actor not found.");
            }
            return actor;
        }

        public ActorModel Add(ActorModel actor)
        {
            var existingActorWithSameRank = _context.Actors.FirstOrDefault(a => a.Rank == actor.Rank);
            if (existingActorWithSameRank != null)
            {
                throw new DuplicateRankException($"Duplicate rank found for  {existingActorWithSameRank.Name}");
            }
            _actors.Add(actor);
            _context.SaveChanges();
            return actor;
        }

        public ActorModel Delete(ActorModel actor)
        {            
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return actor;
        }

        public void Put(ActorModel actor)
        {            
            var existingActorWithSameRank = _context.Actors.FirstOrDefault(a => a.Rank == actor.Rank && a.Id != actor.Id);
            if (existingActorWithSameRank != null)
            {
                throw new DuplicateRankException($"Duplicate rank found for  {existingActorWithSameRank.Name}");
            }
            _context.Entry(actor).State = EntityState.Modified;
            _context.SaveChanges();            
        }
    }
}
