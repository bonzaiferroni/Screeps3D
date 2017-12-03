using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Svg;
// using Svg;
using Svg.Pathing;
using Svg.Transforms;
using UnityEngine;

namespace Screeps3D
{
    public class BadgeManager : MonoBehaviour
    {
        private ScreepsAPI _api;
        private BadgePathGenerator _badgePaths = new BadgePathGenerator();
        private BadgeColorGenerator _badgeColors = new BadgeColorGenerator();
        private Dictionary<string, Texture2D> _badges = new Dictionary<string, Texture2D>();

        public void Init(ScreepsAPI screepsApi)
        {
            _api = screepsApi;
        }

        public void Get(string username, Action<Texture2D> callback)
        {
            if (_badges.ContainsKey(username))
            {
                callback(_badges[username]);
                return;
            }

            var body = new RequestBody();
            body.AddField("username", username);

            _api.Http.Request("GET", "/api/user/badge-svg", body, xml =>
            {
                _badges[username] = Texturize(xml);
                callback(_badges[username]);
            });
        }

        private Texture2D Texturize(string xml)
        {
            var byteArray = Encoding.ASCII.GetBytes(xml);
            using (var stream = new MemoryStream(byteArray))
            {
                var svgDocument = SvgDocument.Open(stream);
                var bitmap = svgDocument.Draw();
                var texture = new Texture2D(bitmap.Width, bitmap.Height);
                for (var x = 0; x < bitmap.Width; x++)
                {
                    for (var y = 0; y < bitmap.Height; y++)
                    {
                        var sysColor = bitmap.GetPixel(x, y);
                        texture.SetPixel(x, y, new Color(sysColor.R / 255f, sysColor.G / 255f, sysColor.B / 255f));
                    }
                }
                texture.Apply();
                return texture;
            }
        }

        public Texture2D Generate(JSONObject badge, float size = 250)
        {
            float pathDefinitionSize = 100;
            float center = pathDefinitionSize / 2;

            var badgeParams = new SvgParams
            {
                param = (int) badge["param"].n,
                flip = badge["flip"].b,
            };

            _badgePaths.Add(badge, badgeParams);
            _badgeColors.Add(badge, badgeParams);

            var rotation = badgeParams.flip ? badgeParams.rotation : 0;

            var sb = new StringBuilder();
            sb.Append(string.Format(
                "<svg width=\"{0}\" height=\"{1}\" viewBox=\"0 0 {2} {3}\" shape-rendering=\"geometricPrecision\">",
                size, size, pathDefinitionSize, pathDefinitionSize));
            sb.Append(string.Format(
                "\n\t<defs><clipPath id=\"#clip\"><circle cx=\"{0}\" cy=\"{1}\" r=\"{2}\" /></clipPath></defs>",
                center, center, pathDefinitionSize / 2));
            sb.Append(string.Format(
                "\n\t<g transform=\"rotate({0} {1} {2})\">", rotation, center, center));
            sb.Append(string.Format(
                "\n\t\t<rect x=\"0\" y=\"0\" width=\"{0}\" height=\"{1}\" fill=\"{2}\" clip-path=\"url(#clip)\"/>",
                pathDefinitionSize, pathDefinitionSize, badgeParams.color1));
            sb.Append(string.Format(
                "\n\t\t<path d=\"{0}\" fill=\"{1}\" clip-path=\"url(#clip)\"/>", badgeParams.path1,
                badgeParams.color2));
            sb.Append(string.Format(
                "\n\t\t<path d=\"{0}\" fill=\"{1}\" clip-path=\"url(#clip)\"/>", badgeParams.path2,
                badgeParams.color3));
            sb.Append("\n\t</g>\n</svg>");

            return Texturize(sb.ToString());
        }

        public void CacheBadge(string id, JSONObject data)
        {
            if (_badges.ContainsKey(id))
                return;
            var badgeObj = data["badge"];
            if (badgeObj == null)
            {
                _badges[id] = null;
                return;
            }

            _badges[id] = Generate(badgeObj);
        }

        public Texture2D GetCached(string creepUserId)
        {
            if (_badges.ContainsKey(creepUserId))
            {
                return _badges[creepUserId];
            } else
            {
                return null;
            }
        }
    }
}