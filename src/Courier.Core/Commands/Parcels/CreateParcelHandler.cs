using Courier.Core.Services;
using System.Threading.Tasks;

namespace Courier.Core.Commands.Parcels
{
    public class CreateParcelHandler : ICommandHandler<CreateParcel>
    {
        private readonly IParcelService parcelService;

        public CreateParcelHandler(IParcelService parcelService)
        {
            this.parcelService = parcelService;
        }
        public async Task HandleAsync(CreateParcel command)
        {
            await parcelService.CreateAsync(command.Id, command.Name, command.SenderId, 
                command.ReceiverId, command.ReceiverAddress);
        }
    }
}
