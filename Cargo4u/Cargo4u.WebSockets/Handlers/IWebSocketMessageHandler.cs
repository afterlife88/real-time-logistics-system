using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4u.WebSockets.Handlers
{
    internal interface IWebSocketMessageHandler : IWebSocketHandler
    {
        string MessageType { get; }
        Task HandleAsync(WebSocketMessage message);
    }
}
