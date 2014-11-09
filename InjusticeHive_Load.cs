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
        protected override void OnLoad(EventArgs e)
        {
            Console.WriteLine("Load");
            /* 
             * Set Initial Options 
             */

            VSync = VSyncMode.Off;

            /*
             * Load Shaders 
             */

            /* Load Vertex Shader */
            int vertexShaderID = GL.CreateShader(ShaderType.VertexShader);   
            string shaderstatus;
            GL.ShaderSource(vertexShaderID,Resources.LoadText("vertexshader.v.glsl"));
            Console.WriteLine("Compiling vertex shader...");
            GL.CompileShader(vertexShaderID);
            GL.GetShaderInfoLog(vertexShaderID, out shaderstatus);
            Console.WriteLine(shaderstatus);

            /* Load Fragment Shader */
            int fragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderID, Resources.LoadText("fragmentshader.f.glsl"));
            Console.WriteLine("Compiling fragment shader...");
            GL.CompileShader(fragmentShaderID);
            GL.GetShaderInfoLog(fragmentShaderID, out shaderstatus);
            Console.WriteLine(shaderstatus);

            /* Link Shaders to Program */
            programID = GL.CreateProgram();
            Console.WriteLine("Attaching shaders...");
            GL.AttachShader(programID, vertexShaderID);
            GL.AttachShader(programID, fragmentShaderID);
            GL.LinkProgram(programID);
            GL.GetProgramInfoLog(programID, out shaderstatus);
            Console.WriteLine(shaderstatus);


            /* Clear Out Shaders */
            GL.DeleteShader(vertexShaderID);
            GL.DeleteShader(fragmentShaderID);

            /*
             * Register Shader Handlers
             */
            mvpID = GL.GetUniformLocation(programID, "MVP");


            /* 
             * Temporary Triangle Creating Code 
             */
            //float[] trianglecoords = {
            //    -1.0f,-1.0f,-1.0f, // triangle 1 : begin
            //    -1.0f,-1.0f, 1.0f,
            //    -1.0f, 1.0f, 1.0f, // triangle 1 : end
            //    1.0f, 1.0f,-1.0f, // triangle 2 : begin
            //    -1.0f,-1.0f,-1.0f,
            //    -1.0f, 1.0f,-1.0f, // triangle 2 : end
            //    1.0f,-1.0f, 1.0f,
            //    -1.0f,-1.0f,-1.0f,
            //    1.0f,-1.0f,-1.0f,
            //    1.0f, 1.0f,-1.0f,
            //    1.0f,-1.0f,-1.0f,
            //    -1.0f,-1.0f,-1.0f,
            //    -1.0f,-1.0f,-1.0f,
            //    -1.0f, 1.0f, 1.0f,
            //    -1.0f, 1.0f,-1.0f,
            //    1.0f,-1.0f, 1.0f,
            //    -1.0f,-1.0f, 1.0f,
            //    -1.0f,-1.0f,-1.0f,
            //    -1.0f, 1.0f, 1.0f,
            //    -1.0f,-1.0f, 1.0f,
            //    1.0f,-1.0f, 1.0f,
            //    1.0f, 1.0f, 1.0f,
            //    1.0f,-1.0f,-1.0f,
            //    1.0f, 1.0f,-1.0f,
            //    1.0f,-1.0f,-1.0f,
            //    1.0f, 1.0f, 1.0f,
            //    1.0f,-1.0f, 1.0f,
            //    1.0f, 1.0f, 1.0f,
            //    1.0f, 1.0f,-1.0f,
            //    -1.0f, 1.0f,-1.0f,
            //    1.0f, 1.0f, 1.0f,
            //    -1.0f, 1.0f,-1.0f,
            //    -1.0f, 1.0f, 1.0f,
            //    1.0f, 1.0f, 1.0f,
            //    -1.0f, 1.0f, 1.0f,
            //    1.0f,-1.0f, 1.0f
            //};
            //float[] trianglecolors = {
            //    0.583f,  0.771f,  0.014f,
            //    0.609f,  0.115f,  0.436f,
            //    0.327f,  0.483f,  0.844f,
            //    0.822f,  0.569f,  0.201f,
            //    0.435f,  0.602f,  0.223f,
            //    0.310f,  0.747f,  0.185f,
            //    0.597f,  0.770f,  0.761f,
            //    0.559f,  0.436f,  0.730f,
            //    0.359f,  0.583f,  0.152f,
            //    0.483f,  0.596f,  0.789f,
            //    0.559f,  0.861f,  0.639f,
            //    0.195f,  0.548f,  0.859f,
            //    0.014f,  0.184f,  0.576f,
            //    0.771f,  0.328f,  0.970f,
            //    0.406f,  0.615f,  0.116f,
            //    0.676f,  0.977f,  0.133f,
            //    0.971f,  0.572f,  0.833f,
            //    0.140f,  0.616f,  0.489f,
            //    0.997f,  0.513f,  0.064f,
            //    0.945f,  0.719f,  0.592f,
            //    0.543f,  0.021f,  0.978f,
            //    0.279f,  0.317f,  0.505f,
            //    0.167f,  0.620f,  0.077f,
            //    0.347f,  0.857f,  0.137f,
            //    0.055f,  0.953f,  0.042f,
            //    0.714f,  0.505f,  0.345f,
            //    0.783f,  0.290f,  0.734f,
            //    0.722f,  0.645f,  0.174f,
            //    0.302f,  0.455f,  0.848f,
            //    0.225f,  0.587f,  0.040f,
            //    0.517f,  0.713f,  0.338f,
            //    0.053f,  0.959f,  0.120f,
            //    0.393f,  0.621f,  0.362f,
            //    0.673f,  0.211f,  0.457f,
            //    0.820f,  0.883f,  0.371f,
            //    0.982f,  0.099f,  0.879f
            //};
            

            /* Set up Vertex Buffer
             * ---------------------
             * Will also need to set
             * up IBO and fix for
             * new mesh system */
            //vertexBufferID = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferID);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(3 * trianglecoords.Length * sizeof(float)), trianglecoords, BufferUsageHint.StaticDraw);
            //GL.InterleavedArrays(InterleavedArrayFormat.V3f, 0, IntPtr.Zero); //Our vertices are shitty and don't specify anything else
            
            /* Set up Color Buffer
             * -------------------
             * Eventually this can
             * be integrated into
             * the vertex buffer
             * by changing
             * InterLeavedArrayFormats */

            //colorBufferID = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, colorBufferID);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(3 * trianglecolors.Length * sizeof(float)),trianglecolors,BufferUsageHint.StaticDraw);
            Scene scene = new Scene();
            Console.WriteLine(ClientRectangle.Width);
            scene.setCamera(new Camera(ClientRectangle.Width, ClientRectangle.Height,45));
            scene.getCamera().setPosition(new Vector3(4f, 3f, 3f));
            setCurrentScene(scene);
            getCurrentScene().init();


            base.OnLoad(e);
        }
    }
}
