﻿using Courier.Core.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courier.Core.Services
{
    public interface IParcelService
    {
        Task CreateAsync(Guid id, string name, Guid senderId, Guid receiverId, string receiverAddress);
        Task<bool> DeliveryAvailableAsync(string address);
        Task<ParcelDetailsDto> GetAsync(Guid id);
        Task<IEnumerable<ParcelDto>> BrowseAsync();
    }
}