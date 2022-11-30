using Newtonsoft.Json;
using ServiceA.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WindTest
{
    public class SiteApiClient
    {
        private const string _siteUri = "http://renewables-codechallenge.azurewebsites.net/api/Site"; 
        
        private static HttpClient client = new HttpClient();

        /// <summary>
        /// Gets all sites an their turbines
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Site>> GetSiteDetailsAsync()
        {
            var sites = await GetSitesAsync();

            foreach (var site in sites)
            {
                var completeSite = await SiteApiClient.GetSiteWithTurbinesAsync(site.Id);

                if (completeSite != null)
                {
                    site.Turbines = completeSite.Turbines;
                    site.Time = DateTime.Now;
                }
            }

            return sites;
        }

        public static async Task<IEnumerable<Site>> GetSitesAsync()
        {
            List<Site> sites;
            HttpResponseMessage response = await client.GetAsync(_siteUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                sites = JsonConvert.DeserializeObject<List<Site>>(json);
            }
            else
            {
                throw new InvalidOperationException($"Excepted status 200, received status {response.IsSuccessStatusCode} from URI '{_siteUri}'");
            }

            return sites ?? new List<Site>();
        }


        public static async Task<Site?> GetSiteWithTurbinesAsync(string siteId)
        {
            Site? site;
            HttpResponseMessage response = await client.GetAsync(_siteUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                site = JsonConvert.DeserializeObject<List<Site>>(json).FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationException($"Excepted status 200, received status {response.IsSuccessStatusCode} from URI '{_siteUri}({siteId})'");
            }

            return site;
        }

    }
}