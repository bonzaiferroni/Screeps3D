using System;
using System.Collections.Generic;

namespace Screeps3D
{
    public class UserManager
    {
        private ScreepsAPI _api;

        private Dictionary<string, ScreepsUser> _users = new Dictionary<string, ScreepsUser>();

        public UserManager(ScreepsAPI screepsApi)
        {
            _api = screepsApi;
        }
    }
}