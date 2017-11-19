namespace Screeps3D {
    
    /*{
        
        "body":[
            {
                "type":"work",
                "hits":100
            },
            {
                "type":"work",
                "hits":100
            },
            {
                "type":"carry",
                "hits":100
            },
            {
                "type":"carry",
                "hits":100
            },
            {
                "type":"move",
                "hits":100
            }
        ],
        "energy":0,
        "energyCapacity":100,
        "type":"creep",
        "room":"E2S7",
        "user":"5a0da017ab17fd00012bf0e7",
        "hits":500,
        "hitsMax":500,
        "spawning":false,
        "fatigue":0,
        "notifyWhenAttacked":true,
        "ageTime":8598,
        "actionLog":{
            "attacked":null,
            "healed":null,
            "attack":null,
            "rangedAttack":null,
            "rangedMassAttack":null,
            "rangedHeal":null,
            "harvest":null,
            "heal":null,
            "repair":null,
            "build":null,
            "say":null,
            "upgradeController":null,
            "reserveController":null
        }
    }*/
    
    internal class Creep : RoomObject {
        
        public string UserId { get; private set; }
        public CreepBody Body { get; private set; }

        internal Creep() {
            Body = new CreepBody();
        }
        
        internal override void Unpack(JSONObject data) {
            base.Unpack(data);
            var userObj = data["user"];
            if (userObj != null) {
                UserId = userObj.str;
            }

            Body.Unpack(Data);
        }
    }
}