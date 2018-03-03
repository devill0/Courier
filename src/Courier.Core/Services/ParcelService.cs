using Courier.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courier.Core.Dto;
using System.Linq;

namespace Courier.Core.Services
{
    public class ParcelService : IParcelService
    {
        private static readonly ISet<Parcel> parcels = new HashSet<Parcel>();
        private readonly ILocationService locationService;

        public ParcelService(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public async Task<IEnumerable<ParcelDto>> BrowseAsync()
        {
            await Task.CompletedTask;

            return parcels.Select(x => new ParcelDto
            {
                Id = x.Id,
                Name = x.Name,
                SentAt = x.SentAt,
                Received = x.RecivedAt.HasValue,              
            });
        }
        public async Task<ParcelDetailsDto> GetAsync(Guid id)
        {
            var parcel = parcels.SingleOrDefault(x => x.Id == id);

            return parcel == null ? null : new ParcelDetailsDto
            {
                Id = parcel.Id,
                Name = parcel.Name,
                SentAt = parcel.SentAt,
                Received = parcel.RecivedAt.HasValue,
                SenderId = parcel.SenderId,
                ReceiverId = parcel.ReciverId,
                ReceivedAt = parcel.RecivedAt
            };
        }

        public async Task CreateAsync(Guid id, string name, Guid senderId, Guid receiverId, string receiverAddress)
        {
            var address = await locationService.GetAsync(receiverAddress);
            if (address == null)
            {
                throw new ArgumentException($"Invalid receiver address: '{receiverAddress}'");
            }
            var sender = GetUser(senderId);
            var receiver = GetUser(receiverId);
            var parcel = new Parcel(id, name, sender, receiver, null,
                Address.Create(address.Latitude, address.Longitude, address.Location));
            parcels.Add(parcel);
        }

        public async Task<bool> DeliveryAvailableAsync(string address)
            => await locationService.ExistsAsync(address);

        private User GetUser(Guid id)
            => new User($"{id}@email.com", "Maricn", "Szatan");
    }
}
