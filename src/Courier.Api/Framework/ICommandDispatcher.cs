using Courier.Core.Commands;
using System.Threading.Tasks;

namespace Courier.Api.Framework
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
