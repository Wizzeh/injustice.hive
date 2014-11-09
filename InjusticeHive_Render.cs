using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Injustice_Hive
{
    partial class InjusticeHive : GameWindow
    {
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            /* ---------------------------
             *        HEY THERE KIDS
             * GET EDUCATED ABOUT MATRICES
             *   ALL THE COOL KIDS DO IT
             * ---------------------------
             * http://www.opengl-tutorial.org/beginners-tutorials/tutorial-3-matrices/ */
            
            //Get the time step since last render
            //It's good to be back rendering!
            //testing gits

            float deltaTime;
            if (!renderTimer.IsRunning)
            {
                deltaTime = 0;
                renderTimer.Start();
            }
            else
            {
                deltaTime = renderTimer.ElapsedMilliseconds;
                renderTimer.Restart(); //careful not to call reset since that will stop the timer
                //Console.WriteLine(1000 * (1 / deltaTime) + " " + this.RenderFrequency);
            }

            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(programID);
            
            
            getCurrentScene().draw(deltaTime);

            this.SwapBuffers(); //Render the scene
            //base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(this.ClientRectangle);
            GL.LoadIdentity();
            base.OnResize(e);
        }
    }
}
