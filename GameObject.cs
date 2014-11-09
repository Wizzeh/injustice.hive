using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Injustice_Hive
{
    class GameObject : Entity, IRenderableEntity, IPhysicsEntity
    {
        private int m_vertexHandle = -1; //invalid uninitiated value

        private int m_indexHandle = -1; //invalid uninitiated value

        private int m_colorHandle = -1;

        public int getIndexHandle()
        {
            return m_indexHandle;
        }

        public int getVertexHandle()
        {
            return m_vertexHandle;
        }

        public GameObject()
            : this(0.0f, 0.0f, 0.0f)
        {

        }

        public GameObject(float x, float y, float z)
        {
            float[] trianglecoords = {
                -1.0f,-1.0f,-1.0f, // triangle 1 : begin
                -1.0f,-1.0f, 1.0f,
                -1.0f, 1.0f, 1.0f, // triangle 1 : end
                1.0f, 1.0f,-1.0f, // triangle 2 : begin
                -1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f,-1.0f, // triangle 2 : end
                1.0f,-1.0f, 1.0f,
                -1.0f,-1.0f,-1.0f,
                1.0f,-1.0f,-1.0f,
                1.0f, 1.0f,-1.0f,
                1.0f,-1.0f,-1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f,-1.0f,
                1.0f,-1.0f, 1.0f,
                -1.0f,-1.0f, 1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f,
                -1.0f,-1.0f, 1.0f,
                1.0f,-1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f,-1.0f,-1.0f,
                1.0f, 1.0f,-1.0f,
                1.0f,-1.0f,-1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f,-1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f,-1.0f,
                1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f, 1.0f,
                1.0f,-1.0f, 1.0f
            };

            for (int i = 0; i < trianglecoords.Length; i++)
            {
                if (i % 3 == 0)
                {
                    trianglecoords[i] += x;
                }
                else if (i % 3 == 1)
                {
                    trianglecoords[i] += y;
                }
                else
                {
                    trianglecoords[i] += z;
                }
            }

            float[] trianglecolors = {
                0.583f,  0.771f,  0.014f,
                0.609f,  0.115f,  0.436f,
                0.327f,  0.483f,  0.844f,
                0.822f,  0.569f,  0.201f,
                0.435f,  0.602f,  0.223f,
                0.310f,  0.747f,  0.185f,
                0.597f,  0.770f,  0.761f,
                0.559f,  0.436f,  0.730f,
                0.359f,  0.583f,  0.152f,
                0.483f,  0.596f,  0.789f,
                0.559f,  0.861f,  0.639f,
                0.195f,  0.548f,  0.859f,
                0.014f,  0.184f,  0.576f,
                0.771f,  0.328f,  0.970f,
                0.406f,  0.615f,  0.116f,
                0.676f,  0.977f,  0.133f,
                0.971f,  0.572f,  0.833f,
                0.140f,  0.616f,  0.489f,
                0.997f,  0.513f,  0.064f,
                0.945f,  0.719f,  0.592f,
                0.543f,  0.021f,  0.978f,
                0.279f,  0.317f,  0.505f,
                0.167f,  0.620f,  0.077f,
                0.347f,  0.857f,  0.137f,
                0.055f,  0.953f,  0.042f,
                0.714f,  0.505f,  0.345f,
                0.783f,  0.290f,  0.734f,
                0.722f,  0.645f,  0.174f,
                0.302f,  0.455f,  0.848f,
                0.225f,  0.587f,  0.040f,
                0.517f,  0.713f,  0.338f,
                0.053f,  0.959f,  0.120f,
                0.393f,  0.621f,  0.362f,
                0.673f,  0.211f,  0.457f,
                0.820f,  0.883f,  0.371f,
                0.982f,  0.099f,  0.879f
            };

            GL.GenBuffers(1, out m_vertexHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertexHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(trianglecoords.Length * sizeof(float)), trianglecoords, BufferUsageHint.DynamicDraw);
            //GL.InterleavedArrays(InterleavedArrayFormat.V3f, 0, IntPtr.Zero);

            m_indexHandle = 999999999; //doing this so it'll work change this code later

            //remove this code when we change vertex data format
            GL.GenBuffers(1,out m_colorHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_colorHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(trianglecolors.Length * sizeof(float)), trianglecolors, BufferUsageHint.StaticDraw);
            //GL.InterleavedArrays(InterleavedArrayFormat., 0, IntPtr.Zero);
        }

        public void draw(float deltatime)
        {
           Scene scene;
           if((scene = getScene()) != null)
           {
                if (m_indexHandle != -1 && m_vertexHandle != -1)
                {
                    Matrix4 Model = Matrix4.Identity;
                    Matrix4 Projview = scene.getCamera().getVPMatrix();
                    Matrix4 MVPs;
                    Matrix4.Mult(ref Projview, ref Model, out MVPs);
                    //
                    GL.UniformMatrix4(InjusticeHive.mvpID, false, ref MVPs);


                    //Set up vertex buffer (0)
                    GL.EnableVertexAttribArray(0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertexHandle);
                    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                    //Set up color buffer (1)
                    GL.EnableVertexAttribArray(1);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_colorHandle);
                    GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

                    GL.DrawArrays(PrimitiveType.Triangles, 0, 12 * 3);
                    //End individual render

                    //Disable vertex buffers
                    GL.DisableVertexAttribArray(0);
                    GL.DisableVertexAttribArray(1);
                }
            }
        }


    }
}
