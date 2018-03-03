using System.Threading.Tasks;
using Courier.Core.Dto;
using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Courier.Core.Services
{
    public class LocationService : ILocationService
    {
        private static readonly Uri apiUrl = new Uri("https://maps.googleapis.com/maps/api/geocode/json");
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = apiUrl
        };

        public async Task<AddressDto> GetAsync(string address)
        {
            var response = await client.GetAsync($"?address={address}&key=AIzaSyD5UaNtOrvxjvxUJscB1qsCfHrPWv6UTtk");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationResponse>(content);
            var result = location.Results?.FirstOrDefault();
          
            return result == null ? null : new AddressDto
            {
                Latitude = result.Geometry.Location.Latitude,
                Longitude = result.Geometry.Location.Longitude,
                Location = result.FormattedAddress
            };
        }

        private class LocationResponse
        {
            public IEnumerable<LocationResult> Results { get; set; }
        }

        private class LocationResult
        {
            [JsonProperty(PropertyName = "formatted_address")]
            public string FormattedAddress { get; set; }
            [JsonProperty(PropertyName = "address_component")]
            public IEnumerable<AddressComponent> AddressComponents { get; set; }
            public Geometry Geometry { get; set; }
        }

        private class AddressComponent
        {
            [JsonProperty(PropertyName = "long_name")]
            public string LongName { get; set; }
            [JsonProperty(PropertyName = "short_name")]
            public string ShortName { get; set; }
            public IEnumerable<string> Types { get; set; }
        }
        private class Geometry
        {
            public GeometryLocation Location { get; set; }
            
        }

        private class GeometryLocation
        {
            [JsonProperty(PropertyName = "lat")]
            public double Latitude { get; set; }
            [JsonProperty(PropertyName = "lng")]
            public double Longitude { get; set; }
        }
    }
}
