using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenTK;

namespace Injustice_Hive
{
    partial class InjusticeHive : OpenTK.GameWindow
    {
        protected override void OnUpdateFrame(OpenTK.FrameEventArgs e)
        {
            OpenTK.Input.KeyboardState keystate = OpenTK.Input.Keyboard.GetState();
            Vector3 transform = new Vector3(0f, 0.001f, 0f);
            Vector3 current = getCurrentScene().getCamera().getPosition();
            Vector3 final;
            Vector3.Add(ref current, ref transform, out final);
            getCurrentScene().getCamera().setPosition(final);
        }
    }
}
