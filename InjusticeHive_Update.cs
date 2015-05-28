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

            int x, y;

            if (keystate.IsKeyDown(OpenTK.Input.Key.Right))
            {
                if (keystate.IsKeyDown(OpenTK.Input.Key.Left))
                {
                    x = 0;
                }
                else
                {
                    x = 1;
                }
            }
            else if (keystate.IsKeyDown(OpenTK.Input.Key.Left))
            {
                x = -1;
            }
            else
            {
                x = 0;
            }

            if (keystate.IsKeyDown(OpenTK.Input.Key.Up))
            {
                if (keystate.IsKeyDown(OpenTK.Input.Key.Down))
                {
                    y = 0;
                }
                else
                {
                    y = 1;
                }
            }
            else if (keystate.IsKeyDown(OpenTK.Input.Key.Down))
            {
                y = -1;
            }
            else
            {
                y = 0;
            }

            OrbitCamera camera = ((OrbitCamera)getCurrentScene().getCamera());
            Vector3 current = camera.getPositionSpherical();
            //(current.Z > Math.PI ? -1 : 1) *
            Vector3 transform = new Vector3(0,  x * 1.0f * (float)e.Time, y * -1.0f * (float)e.Time);
            Vector3 final;
            Vector3.Add(ref current, ref transform, out final);
            camera.setPositionSpherical(new Vector3(current.X,current.Y+transform.Y,(float)Math.Max(Math.Min(current.Z+transform.Z,Math.PI-.01f),0.01f))); //This is a bit hacky because of a rendering issue
        }
    }
}
