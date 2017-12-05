using UnityEngine;

namespace Screeps3D
{
    public class ControllerView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private Renderer _rend;
        private Texture2D _texture;
        private Color _controllerWhite;
        private Controller _controller;

        public void Init()
        {
            InitTexture();
        }

        public void Load(RoomObject roomObject)
        {
            _controller = roomObject as Controller;
            UpdateTexture();
        }

        private void InitTexture()
        {
            _texture = new Texture2D(8, 1);
            _texture.filterMode = FilterMode.Point;
            _rend.materials[0].mainTexture = _texture;
            ColorUtility.TryParseHtmlString("#FDF5E6", out _controllerWhite);
        }

        public void Delta(JSONObject data)
        {
            if (data["level"] == null)
                return;
            UpdateTexture();
        }

        public void Unload(RoomObject roomObject)
        {
        }

        private void UpdateTexture()
        {
            var level = 0;
            for (var i = 0; i < 8; i++)
            {
                if (level < _controller.Level)
                {
                    _texture.SetPixel(i, 1, _controllerWhite);
                } else
                {
                    _texture.SetPixel(i, 1, Color.black);
                }
                level++;
            }
            _texture.Apply();
        }
    }
}