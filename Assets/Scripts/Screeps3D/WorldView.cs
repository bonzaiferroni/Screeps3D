using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Screeps3D
{
    public class WorldView : BaseSingleton<WorldView>
    {
        private const int viewDistance = 2;

        [SerializeField] private GameObject roomPrefab;
        [SerializeField] private RoomChooser chooser;
        [SerializeField] private PlayerGaze playerGaze;

        private Dictionary<string, RoomView> visibleRooms = new Dictionary<string, RoomView>();
        private Stack<WorldCoord> loadStack = new Stack<WorldCoord>();
        private Stack<RoomView> preloadStack = new Stack<RoomView>();

        private void Start()
        {
            chooser.OnChooseRoom += OnChoose;
            GrowPreload(30);
        }

        private void OnChoose(WorldCoord coord)
        {
            loadStack.Push(coord);
            TransportPlayer(coord.vector);
        }

        private void GenerateRoom(WorldCoord coord)
        {
            if (visibleRooms.ContainsKey(coord.key))
            {
                return;
            }

            if (preloadStack.Count == 0)
            {
                GrowPreload(1);
            }

            var view = preloadStack.Pop();
            view.Show();
            view.gameObject.name = coord.key;
            visibleRooms[coord.key] = view;
            view.transform.localPosition = coord.vector;
            view.Load(coord);
        }

        private void GrowPreload(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var go = Instantiate(roomPrefab);
                var view = go.GetComponent<RoomView>();
                view.transform.SetParent(transform);
                preloadStack.Push(view);
            }
        }

        private void TransportPlayer(Vector3 pos)
        {
            playerGaze.transform.position = new Vector3(pos.x + 25, pos.y, pos.z + 25);
        }

        public void LoadNeighbors(WorldCoord coord)
        {
            for (var xDelta = -viewDistance; xDelta <= viewDistance; xDelta++)
            {
                for (var yDelta = -viewDistance; yDelta <= viewDistance; yDelta++)
                {
                    if (xDelta == 0 && yDelta == 0) continue;
                    var relativeCoord = coord.Relative(xDelta, yDelta);
                    if (visibleRooms.ContainsKey(relativeCoord.key))
                        continue;
                    loadStack.Push(relativeCoord);
                }
            }
        }

        private void Update()
        {
            var time = Time.time;
            while (loadStack.Count > 0 && Time.time - time < .01f)
            {
                var coord = loadStack.Pop();
                GenerateRoom(coord);
                return;
            }

            if (preloadStack.Count < 30)
            {
                GrowPreload(1);
            }
        }
    }
}