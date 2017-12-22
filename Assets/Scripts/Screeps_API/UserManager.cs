using System.Collections.Generic;
using UnityEngine;

namespace Screeps_API
{
    public class UserManager
    {
        private Dictionary<string, ScreepsUser> users = new Dictionary<string, ScreepsUser>();

        public ScreepsUser GetUser(string id)
        {
            if (id == null)
                return null;
            if (users.ContainsKey(id))
            {
                return users[id];
            } else
            {
                return null;
            }
        }
        
        internal ScreepsUser CacheUser(JSONObject data)
        {

            var id = data["_id"].str;
            
            if (users.ContainsKey(id)) return users[id];

            Texture2D badge = null;
            var isNpc = false;
            var badgeData = data["badge"];
            if (badgeData != null)
            {
                badge = ScreepsAPI.Badges.Generate(badgeData);
            } 
            else
            {
                isNpc = true;
                badge = ScreepsAPI.Badges.Invader;
            }

            var username = "unknown"; 
            var nameData = data["username"];
            if (nameData != null)
            {
                username = nameData.str;
            }

            var cpu = 10;
            var cpuData = data["cpu"];
            if (cpuData != null)
            {
                cpu = (int) cpuData.n;
            }

            var user = new ScreepsUser(id, username, cpu, badge, isNpc);
            users[id] = user;
            return user;
        }
    }
}