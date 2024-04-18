using ActorsWebApplication.Models;
using HtmlAgilityPack;
using System.Collections.Specialized;

namespace ActorsWebApplication.DataProvidors
{
    public class IMDbDataProvider : IActorDataProvider
    {        
        public List<ActorModel> ScrapeActors()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://www.imdb.com/list/ls054840033/");

            var actorNodes = doc.DocumentNode.SelectNodes("//div[@class='lister-item mode-detail']");
            List<ActorModel> actors = new List<ActorModel>();

            foreach (var actorNode in actorNodes)
            {
                var nameNode = actorNode.SelectSingleNode(".//h3[@class='lister-item-header']/a");
                var rankNode = actorNode.SelectSingleNode(".//span[@class='lister-item-index unbold text-primary']");
                var typeNode = actorNode.SelectSingleNode(".//p[@class='text-muted text-small']/text()[1]");
                var detailsNode = actorNode.SelectSingleNode(".//div[@class='list-description']/p[1]");
                string details=null;
                if (detailsNode?.InnerText != null)
                {
                    details = HtmlEntity.DeEntitize(detailsNode?.InnerText).Trim();
                }
                
                var type = typeNode?.InnerText.Trim();
                var name = nameNode?.InnerText.Trim();
                var rank = rankNode?.InnerText.Trim().TrimEnd('.');

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(rank))
                {
                    var ra = int.Parse(rank);
                    actors.Add(new ActorModel { Name = name, Rank = ra, Source = Source.IMDB.ToString(), Details = details, Type = type });
                }
            }

            return actors;
        }

    }
}
