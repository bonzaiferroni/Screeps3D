using System;
using System.Collections.Generic;

namespace Screeps_API
{
    [Serializable]
    public class ServerCache
    {
        public Address Address = new Address();
        public Credentials Credentials = new Credentials();
        public Dictionary<string, string> Terrain = new Dictionary<string, string>();
        public bool SaveCredentials;
    }
}