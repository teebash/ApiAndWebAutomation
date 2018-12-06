using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace ApiTesting.Base
{
    public class CountryDataLayer
    {
        private readonly Utils.Http _http = new Utils.Http();
        private static readonly string BaseRequestUrl = ConfigurationManager.AppSettings.Get("baseurl");

        private async Task<IList<T>> GetMethod<T>(string requestEndpoint)
        {
            var response = await _http.GetMethod<T>($"{BaseRequestUrl}/{requestEndpoint}");
            return response;
        }

        public async Task<IList<T>> GetCountryDataByCapitalName<T>(string capitalCity)
        {
            var fullResquestEndpoint = $"capital/{capitalCity}";

            var response = await GetMethod<T>(fullResquestEndpoint);
            return response;
        }

        public async Task<IList<T>> GetCountryDataByRegion<T>(string geographicalRegionName)
        {
            var requestUri = $"region/{geographicalRegionName}";
            var response = await GetMethod<T>(requestUri);
            return response;
        }
    }
}
