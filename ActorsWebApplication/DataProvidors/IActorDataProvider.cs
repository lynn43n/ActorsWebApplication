using ActorsWebApplication.Models;
using System.Collections.Specialized;

namespace ActorsWebApplication.DataProvidors
{
    public interface IActorDataProvider
    {
        List<ActorModel> ScrapeActors();
    }
}
