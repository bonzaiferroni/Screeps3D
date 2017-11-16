using System;
using System.Collections.Generic;

namespace Screeps3D {
    public class UserManager {
        private ScreepsAPI api;

        private Dictionary<string, ScreepsUser> users = new Dictionary<string, ScreepsUser>();

        public UserManager(ScreepsAPI screepsApi) {
            api = screepsApi;
        }
    }
}