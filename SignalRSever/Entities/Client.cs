using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSever
{
    public class Client
    {
        public string connectionId { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int point { get; set; }
        public Client opponent { get; set; }
        public bool isReady { get; set; }
        public bool lookingForOpponent { get; set; }
        
    }
}