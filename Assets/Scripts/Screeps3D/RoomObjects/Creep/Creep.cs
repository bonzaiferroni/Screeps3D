using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
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

    internal class Creep : RoomObject, IEnergyObject, INamedObject, IHitpointsObject, IOwnedObject
    {
        public string UserId { get; set; }
        public ScreepsUser Owner { get; set; }
        public CreepBody Body { get; private set; }
        public string Name { get; set; }
        public Dictionary<string, JSONObject> Actions { get; private set; }
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }
        public float Hits { get; set; }
        public float HitsMax { get; set; }
        public float Fatigue { get; set; }
        public float TTL { get; set; }
        public float AgeTime { get; set; }

        internal Creep()
        {
            Body = new CreepBody();
            Actions = new Dictionary<string, JSONObject>();
        }

        internal override void Unpack(JSONObject data)
        {
            base.Unpack(data);

            var userObj = data["user"];
            if (userObj != null)
            {
                UserId = userObj.str;
            }

            var actionObj = data["actionLog"];
            if (actionObj != null)
            {
                foreach (var key in actionObj.keys)
                {
                    Actions[key] = actionObj[key];
                }
            }

            var ageData = data["ageTime"];
            if (ageData != null)
            {
                AgeTime = ageData.n;
            }
            
            var fatigueData = data["fatigue"];
            if (fatigueData != null)
            {
                Fatigue = fatigueData.n;
            }

            UnpackUtility.Energy(this, data);
            UnpackUtility.Name(this, data);
            UnpackUtility.HitPoints(this, data);
            UnpackUtility.Owner(this, data);

            Body.Unpack(Data);
        }

        public override void EnterRoom(EntityView entityView)
        {
            if (View != null)
            {
                View.transform.SetParent(entityView.transform, true);
            } else
            {
                View = ObjectViewer.Instance.NewView(this);
                View.transform.SetParent(entityView.transform, false);
                View.Init(this);
            }
            
            if (View != null)
            {
                View.Show();
                if (OnShow != null) OnShow(View);
            }
        }

        public override void LeaveRoom(EntityView entityView)
        {
            if (View == null || entityView.Coord.roomName != RoomName)
            {
                return;
            }
            View.Hide();
        }
    }
}