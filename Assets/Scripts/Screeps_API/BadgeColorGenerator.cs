using Screeps3D;

namespace Screeps_API
{
    internal class BadgeColorGenerator
    {
        private string[] _colors =
        {
            "#cccccc", "#ebadad", "#ebc1ad", "#ebd4ad", "#ebe7ad", "#daebad", "#c7ebad", "#b4ebad", "#adebba",
            "#adebce", "#adebe1", "#ade1eb", "#adceeb", "#adbaeb", "#b4adeb", "#c7adeb", "#daadeb", "#ebade7",
            "#ebadd4", "#ebadc1", "#808080", "#d92626", "#d95f26", "#d99726", "#d9cf26", "#aad926", "#71d926",
            "#39d926", "#26d94c", "#26d984", "#26d9bd", "#26bdd9", "#2684d9", "#264cd9", "#3926d9", "#7126d9",
            "#aa26d9", "#d926cf", "#d92697", "#d9265f", "#4d4d4d", "#6b2e2e", "#6b412e", "#6b552e", "#6b682e",
            "#5b6b2e", "#486b2e", "#346b2e", "#2e6b3b", "#2e6b4e", "#2e6b61", "#2e616b", "#2e4e6b", "#2e3b6b",
            "#342e6b", "#482e6b", "#5b2e6b", "#6b2e68", "#6b2e55", "#6b2e41", "#1a1a1a", "#260d0d", "#26150d",
            "#261d0d", "#26250d", "#20260d", "#17260d", "#0f260d", "#0d2612", "#0d261a", "#0d2622", "#0d2226",
            "#0d1a26", "#0d1226", "#0f0d26", "#170d26", "#200d26", "#260d25", "#260d1d", "#260d15"
        };

        public void Add(JSONObject badge, SvgParams badgeParams)
        {
            if (badge["color1"].IsNumber)
            {
                LookUp(badge, badgeParams);
            } else
            {
                Assign(badge, badgeParams);
            }
        }

        public void Assign(JSONObject badge, SvgParams badgeParams)
        {
            badgeParams.color1 = badge["color1"].str;
            badgeParams.color2 = badge["color2"].str;
            badgeParams.color3 = badge["color3"].str;
        }

        public void LookUp(JSONObject badge, SvgParams badgeParams)
        {
            badgeParams.color1 = _colors[(int) badge["color1"].n];
            badgeParams.color2 = _colors[(int) badge["color2"].n];
            badgeParams.color3 = _colors[(int) badge["color3"].n];
        }
    }
}