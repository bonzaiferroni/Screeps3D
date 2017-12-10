using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class CreepBodyView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private Renderer _rend;
        private Creep _creep;
        private Texture2D _texture;
        private Color _rangedAttackColor;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            if (_texture == null)
            {
                InitTexture();
            }
            _creep = roomObject as Creep;
            UpdateView();
        }

        private void InitTexture()
        {
            ColorUtility.TryParseHtmlString("#4a708b", out _rangedAttackColor);
            _texture = new Texture2D(50, 1);
            _texture.filterMode = FilterMode.Point;
            _rend.material.mainTexture = _texture;
        }

        public void Delta(JSONObject data)
        {
            var bodyObj = data["body"];
            if (bodyObj == null)
                return;
            UpdateView();
        }

        public void Unload(RoomObject roomObject)
        {
        }

        private void UpdateView()
        {
            var frontIndex = 0;
            for (var i = 0; i < PartCount("ranged_attack"); i++)
            {
                _texture.SetPixel(frontIndex, 0, _rangedAttackColor);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("attack"); i++)
            {
                _texture.SetPixel(frontIndex, 0, Color.red);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("heal"); i++)
            {
                _texture.SetPixel(frontIndex, 0, Color.green);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("work"); i++)
            {
                _texture.SetPixel(frontIndex, 0, Color.yellow);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("claim"); i++)
            {
                _texture.SetPixel(frontIndex, 0, Color.magenta);
                frontIndex++;
            }

            var backIndex = 0;
            for (; backIndex < PartCount("move"); backIndex++)
            {
                _texture.SetPixel(49 - backIndex, 0, Color.gray);
            }

            var toughAlpha = Mathf.Min(PartCount("tough") / 10f, 1);
            var toughColor = new Color(1, 1, 1, toughAlpha);
            for (; frontIndex < 49 - (backIndex - 1); frontIndex++)
            {
                _texture.SetPixel(frontIndex, 0, toughColor);
            }
            _texture.Apply();
        }

        private int PartCount(string type)
        {
            var count = 0;
            foreach (var part in _creep.Body.Parts)
            {
                if (part.type == type && part.hits > 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}