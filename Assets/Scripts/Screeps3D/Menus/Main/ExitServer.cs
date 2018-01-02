using Screeps_API;

namespace Screeps3D.Menus.Main
{
    public class ExitServer : MainMenuItem
    {
        public override string Description
        {
            get { return "Exit Server"; }
        }

        public override void Invoke()
        {
            ScreepsAPI.Instance.Disconnect();
            GameManager.ChangeMode(GameMode.Login);
        }
    }
}