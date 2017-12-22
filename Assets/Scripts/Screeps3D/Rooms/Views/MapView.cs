using System.Collections.Generic;
using Common;
using Screeps_API;
using UnityEngine;

namespace Screeps3D.Rooms.Views
{
    /*
         {
             "w":[],                                                                // walls
             "r":[[40,31],[36,2],[40,30],[40,33],[37,3],[38,4],[39,34],[38,5]],     // road
             "pb":[],                                                               // powerbank
             "p":[],                                                                // power
             "s":[[35,4],[42,36]],                                                  // source
             "c":[[9,33]],                                                          // controller
             "m":[[29,45]],                                                         // mineral
             "k":[],                                                                // keeper
             "cd74623069821ed":[[32,6],[35,3],[41,40],[41,37],[10,33]],             // player
             "f4b532d08c3952a":[[32,18],[33,17],[32,16],[31,15],[33,16]]            // player
         }
     */
    
    public class MapView : MonoBehaviour, IRoomViewComponent
    {
        public Room Room { get; private set; }

        private MapDotView[,] _dots = new MapDotView[50, 50];
        private List<MapDotView> _dotList = new List<MapDotView>();
        
        public void Init(Room room)
        {
            Room = room;
            Room.MapStream.OnData += OnMapData;
            Room.OnShowObjects += OnShowObjects;
        }

        private void OnShowObjects(bool show)
        {
            if (!show)
                return;
            ClearDots();
        }

        private void OnMapData(JSONObject data)
        {
            ClearDots();

            if (Room.ShowingObjects)
                return;
            
            SpawnDots(data);
        }

        private void SpawnDots(JSONObject data)
        {
            foreach (var key in data.keys)
            {
                if (key.Length <= 2)
                    continue;
                
                var color = key == ScreepsAPI.Me.UserId ? Color.green : Color.red;
                foreach (var numArray in data[key].list)
                {
                    var x = (int) numArray.list[0].n;
                    var y = (int) numArray.list[1].n;
                    var view = _dots[x, y];
                    if (!view || view.Color != color)
                    {
                        var go = PoolLoader.Load(MapDotView.Path);
                        view = go.GetComponent<MapDotView>();
                    }
                    
                    view.Load(x, y, this);
                    view.Color = color;
                    view.Show();
                    _dots[x, y] = view;
                    _dotList.Add(view);
                }
            }
        }

        private void ClearDots()
        {
            foreach (var dot in _dotList)
            {
                dot.Hide();
            }
            _dotList.Clear();
        }

        public void RemoveAt(int x, int y)
        {
            _dots[x, y] = null;
        }
    }
}