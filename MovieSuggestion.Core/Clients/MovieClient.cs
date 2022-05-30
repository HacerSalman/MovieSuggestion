using Microsoft.Extensions.Caching.Memory;
using MovieSuggestion.Data.Utils.MovieSuggestion.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Clients
{
    public class MovieClient
    {
        private readonly HttpClient _client = null;
        private readonly string apiKey = EnvironmentVariable.GetConfiguration().MovieClientApiKey;

        public MovieClient(HttpClient client)
        {
            _client = client;
        }

        public T SendAndReceiveJson<T>(string endPoint, Dictionary<string, string> input) where T : MovieClientBaseResponse
        {
            try
            {
                var contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
                HttpResponseMessage response;
                switch (endPoint)
                {                    
                    case MovieClientEndpoints.ListNowPlaying:
                        string query;
                        input.Add("api_key", apiKey);
                        using (var content = new FormUrlEncodedContent(input))
                        {
                            query = content.ReadAsStringAsync().Result;
                        }
                        var uri = new Uri(EnvironmentVariable.GetConfiguration().MovieClientBaseUrl + endPoint + "?" + query);
                        response =  _client.GetAsync(uri).Result;
                        break;
                    
                    default:
                        throw new InvalidOperationException();
                }
                var result =  response.Content.ReadAsStringAsync().Result;
               return JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings() { ContractResolver = contractResolver });
                
            }
            catch (Exception)
            {          
                throw;
            }
        }

        public MovieClientBaseResponse ListNowPlaying(Dictionary<string, string> input)
        {
            return  SendAndReceiveJson<MovieClientBaseResponse>(MovieClientEndpoints.ListNowPlaying, input);
        }
        public static class MovieClientEndpoints
        {
            public const string ListNowPlaying = "movie/now_playing";
        }

        public class MovieClientBaseResponse
        {
            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("total_results")]
            public int TotalResults { get; set; }

            [JsonProperty("total_pages")]
            public int TotalPages { get; set; }

            [JsonProperty("results")]
            public List<MovieList> Results { get; set; }
        }

        public class MovieList
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("original_title")]
            public string OriginalTitle { get; set; }

            [JsonProperty("overview")]
            public string Overview { get; set; }

            [JsonProperty("score")]
            public double Score { get; set; }

            [JsonProperty("adult")]
            public bool Adult { get; set; }

            [JsonProperty("backdrop_path")]
            public string BackdropPath { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("original_language")]
            public string OriginalLanguage { get; set; }

            [JsonProperty("popularity")]
            public double Popularity { get; set; }

            [JsonProperty("poster_path")]
            public string PosterPath { get; set; }


            [JsonProperty("video")]
            public bool Video { get; set; }
        }
    }
}
