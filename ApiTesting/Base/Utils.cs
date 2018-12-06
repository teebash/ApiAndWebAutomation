using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiTesting.Base
{
    public class Utils
    {
        public class Http
        {
            private readonly HttpClient _httpClient;
            private const string DefaultContentType = "application/json";

            private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            public Http()
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultContentType));
            }

            public async Task<IList<T>> GetMethod<T>(string requestUrl)
            {
                var httpRequestMsg = await _httpClient.GetAsync(requestUrl);
                if (httpRequestMsg.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpException("Request failed with : " + httpRequestMsg);
                }

                var responsonJson = await httpRequestMsg.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<IList<T>>(responsonJson, _serializerSettings);
                return responseObj;
            }
        }
    }
}
