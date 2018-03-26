using System;

namespace Cargo4u.WebSockets.Handlers
{
    internal interface IWebSocketHandler
    {
        event CallbackMessageEventHandler CallbackMessage;
    }

    internal delegate void CallbackMessageEventHandler(object sender, CallbackMessageEventHandlerArgs args);

    internal class CallbackMessageEventHandlerArgs : EventArgs
    {
        public Receivers Receivers { get; }
        public WebSocketMessage Message { get; }

        public CallbackMessageEventHandlerArgs(WebSocketMessage message, Receivers receivers)
        {
            Receivers = receivers;
            Message = message;
        }
    }
}
