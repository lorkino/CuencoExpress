using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nest.JsonNetSerializer;
using Film.Models;

namespace Film.Controllers
{
    public static class MyGlobals
    {
        private readonly static  IEnumerable<Uri> uris = new[]
        {
            new Uri("http://localhost:9200"),
            new Uri("http://localhost:9201"),
            new Uri("http://localhost:9202"),
        };

        private readonly static SniffingConnectionPool connectionPool = new SniffingConnectionPool(uris);
        private readonly static ConnectionSettings settings = new ConnectionSettings(connectionPool,(builtInSerializer, connectionSettings) =>
        new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        })).DefaultIndex("people").DisableDirectStreaming();


        public static ElasticClient elasticClient = new ElasticClient(settings);

        public static void SearchByTags(List<Knowledges> knowledges) {

            var searchRequest1 = elasticClient.Search<User>(s =>
             s.Query(q => q
                .MatchAll()
                ).AllTypes().Index("people"));

            List<QueryContainer> mustClauses = new List<QueryContainer>();
         

            knowledges.ForEach(delegate (Knowledges knowledge)
             {
                 mustClauses.Add(new MatchQuery()
                 {
                     Field = new Field("userKnowledges.knowledges.value"),
                     Query = knowledge.Value,
                     Operator=Operator.Or
                 });
             });
            var searchRequest = new SearchRequest<User>("people")
            {
                Size = 10,
                From = 0,
                Query = new BoolQuery
                {                 
                    Should = mustClauses
                }
            };
            var searchResponse = elasticClient.Search<User>(searchRequest);

           var listUser = searchResponse.Documents.Distinct().ToList();
           

        }
    }
}
