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
        private readonly static IEnumerable<Uri> uris;
        private readonly static StaticConnectionPool connectionPool;
        private readonly static ConnectionSettings settings;
        public static ElasticClient elasticClient;

        static MyGlobals() {

            try
            {
                var settings = new ConnectionSettings(new Uri("http://localhost.com:9200"));

                var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

                settings = new ConnectionSettings(pool, (builtInSerializer, connectionSettings) =>
                    new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })).DefaultIndex("peoplev5")
        .DisableDirectStreaming();
                elasticClient = new ElasticClient(settings);
                elasticClient.Map<User>(a => a.AutoMap());
           }
            catch (Exception e) {
            }
        }



        
        public static  List<User> SearchByTags(List<Knowledges> knowledges) {
            //if (elasticClient.IndexExists("people").Exists)
            //    elasticClient.DeleteIndex("people");


            //var createIndexResponse = elasticClient.CreateIndex("people", c => c
            //    .Mappings(m => m
            //        .Map<User>(mm => mm
            //            .AutoMap()
            //            .Properties(p => p
            //                .GeoPoint(g => g
            //                    .Name(n => n.UserDates.Location)
            //                )
            //            )
            //        )
            //    )
            //);
            var searchRequest1 = elasticClient.Search<User>(s =>
             s.Query(q => q
                .MatchAll()
                ).Index("peoplev5"));

            List<QueryContainer> mustClauses = new List<QueryContainer>();


            //knowledges.ForEach(delegate (Knowledges knowledge)
            // {
            //     mustClauses.Add(new MatchQuery()
            //     {
            //         Field = new Field("userKnowledges.knowledges.value"),
            //         Query = knowledge.Value,
            //         Operator = Operator.Or
            //     });
            // });
            GeoDistanceQuery a = new GeoDistanceQuery
            {
                Boost = 1.1,
                Name = "named_query",
                Field = new Field("location"),
                DistanceType = GeoDistanceType.Arc,
                Location = new Nest.GeoLocation(0, 0),
                Distance = "2000m",
                ValidationMethod = GeoValidationMethod.IgnoreMalformed
                
                
                
            };
            
            mustClauses.Add(a);
            var searchRequest = new SearchRequest<User>("peoplev5")
            {
                Size = 10,
                From = 0,
                Query = new BoolQuery
                {                 
                    Should = mustClauses
                    
                    
                }
            };
           
            var searchResponse = elasticClient.Search<User>(searchRequest);

            return searchResponse.Documents.Distinct().ToList();

        }
    }
}
