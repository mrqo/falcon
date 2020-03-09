using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Falcon.Server.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        public List<int> Clients { get; set; } = new List<int>();

        public static Session Create()
        {
            LastSessionId++;

            return new Session
            {
                SessionId = LastSessionId
            };
        }

        protected static int LastSessionId = 0;
    }
}
