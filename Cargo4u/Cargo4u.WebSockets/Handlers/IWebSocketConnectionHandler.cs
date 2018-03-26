using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4u.WebSockets.Handlers
{
    internal interface IWebSocketConnectionHandler : IWebSocketHandler
    {
        Task NotifyConnectedAsync();
        Task NotifyDisconnectedAsync();
    }
}
