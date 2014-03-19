using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRSever
{
    public class chatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update listPlay.
            Clients.All.broadcastMessage(name, message);
        }
    }
}