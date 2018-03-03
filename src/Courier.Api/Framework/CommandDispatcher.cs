using System.Threading.Tasks;
using Courier.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Courier.Api.Framework
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ServiceProvider serviceProvider;

        public CommandDispatcher(IServiceCollection serviceCollection)
        {
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var handler = serviceProvider.GetService<ICommandHandler<T>>();
            if(handler == null)
            {
                throw new ArgumentException($"Command handler: '{typeof(T).Name}' was not found.", nameof(handler));
            }
            await handler.HandleAsync(command);
        }
    }
}
