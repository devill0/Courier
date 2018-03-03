using Courier.Core.Dto;
using System.Threading.Tasks;

namespace Courier.Core.Services
{
    public interface ILocationService
    {
        Task<AddressDto> GetAsync(string address);
        Task<bool> ExistsAsync(string address);
    }
}
