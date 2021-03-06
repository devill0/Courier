﻿using System;

namespace Courier.Core.Domain
{
    public class Parcel
    {
        public Guid Id { get; private set; }
        public Guid SenderId { get; private set; }
        public Guid ReciverId { get; private set; }
        public string Name { get; private set; }
        public DateTime SentAt { get; private set; }
        public DateTime? RecivedAt { get; private set; }
        public Address SenderAddress { get; private set; }
        public Address ReceiverAddress { get; private set; }
        public ParcelStatus Status { get; private set; }

        public Parcel(Guid id, string name, User sender, User receiver, 
            Address senderAdders, Address receiverAddress)
        {
            Id = id;
            Name = name;
            SenderId = sender.Id;
            ReciverId = receiver.Id;
            SentAt = DateTime.UtcNow;
            SenderAddress = senderAdders;
            ReceiverAddress = receiverAddress;
            Status = ParcelStatus.Sent;
        }
    }
}
