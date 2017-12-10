using UnityEngine;

namespace Screeps_API
{
    public class ScreepsUser
    {
        public readonly string userId;
        public readonly string username;
        public readonly int cpu;
        public readonly Texture2D badge;
        public readonly bool isNpc;

        public ScreepsUser(string userId, string username, int cpu, Texture2D badge, bool isNpc)
        {
            this.userId = userId;
            this.username = username;
            this.cpu = cpu;
            this.badge = badge;
            this.isNpc = isNpc;
        }
    }
}