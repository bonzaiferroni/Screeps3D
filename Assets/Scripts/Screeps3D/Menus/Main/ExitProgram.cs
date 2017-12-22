using UnityEngine;

namespace Screeps3D.Menus.Main
{
    public class ExitProgram : MainMenuItem
    {
        public override string Description
        {
            get { return "Exit Program"; }
        }

        public override void Invoke()
        {
            Application.Quit();
        }
    }
}