using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Screeps3D
{
    public class WorldView : BaseSingleton<WorldView>
    {
        private const int VIEW_DISTANCE = 2;

        [SerializeField] private GameObject _roomPrefab;
        [SerializeField] private RoomChooser _chooser;
        [SerializeField] private PlayerGaze _playerGaze;

        private Dictionary<string, RoomView> _visibleRooms = new Dictionary<string, RoomView>();
        private Stack<WorldCoord> _loadStack = new Stack<WorldCoord>();
        private Stack<RoomView> _preloadStack = new Stack<RoomView>();

        private void Start()
        {
            _chooser.OnChooseRoom += OnChoose;
            GrowPreload(30);
        }

        private void OnChoose(WorldCoord coord)
        {
            _loadStack.Push(coord);
            TransportPlayer(coord.vector);
        }

        private void GenerateRoom(WorldCoord coord)
        {
            if (_visibleRooms.ContainsKey(coord.key))
            {
                return;
            }

            if (_preloadStack.Count == 0)
            {
                GrowPreload(1);
            }

            var view = _preloadStack.Pop();
            view.Show();
            view.gameObject.name = coord.key;
            _visibleRooms[coord.key] = view;
            view.transform.localPosition = coord.vector;
            view.Load(coord);
        }

        private void GrowPreload(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var go = Instantiate(_roomPrefab);
                var view = go.GetComponent<RoomView>();
                view.transform.SetParent(transform);
                _preloadStack.Push(view);
            }
        }

        private void TransportPlayer(Vector3 pos)
        {
            _playerGaze.transform.position = new Vector3(pos.x + 25, pos.y, pos.z + 25);
        }

        public void LoadNeighbors(WorldCoord coord)
        {
            for (var xDelta = -VIEW_DISTANCE; xDelta <= VIEW_DISTANCE; xDelta++)
            {
                for (var yDelta = -VIEW_DISTANCE; yDelta <= VIEW_DISTANCE; yDelta++)
                {
                    if (xDelta == 0 && yDelta == 0) continue;
                    var relativeCoord = coord.Relative(xDelta, yDelta);
                    if (_visibleRooms.ContainsKey(relativeCoord.key))
                        continue;
                    _loadStack.Push(relativeCoord);
                }
            }
        }

        private void Update()
        {
            var time = Time.time;
            while (_loadStack.Count > 0 && Time.time - time < .01f)
            {
                var coord = _loadStack.Pop();
                GenerateRoom(coord);
                return;
            }

            if (_preloadStack.Count < 30)
            {
                GrowPreload(1);
            }
        }
    }
}