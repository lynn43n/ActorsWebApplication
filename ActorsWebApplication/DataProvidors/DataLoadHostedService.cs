using ActorsWebApplication.Data;
using ActorsWebApplication.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ActorsWebApplication.DataProvidors
{
    public class DataLoadHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataLoadHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var actorDataProviders = scope.ServiceProvider.GetServices<IActorDataProvider>();
                var context = scope.ServiceProvider.GetRequiredService<ApiContext>();

                foreach (var actorDataProvider in actorDataProviders)
                {
                    var actors = actorDataProvider.ScrapeActors();
                    context.Actors.AddRange(actors);
                }
                await context.SaveChangesAsync();
            }
        }       

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
