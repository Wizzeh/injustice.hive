using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Injustice_Hive
{
    class Camera : Entity
    {

        private GameObject m_character = null;

        public Camera(float width,float height,int fov)
        {
            m_FOV = fov;
            m_width = width;
            m_height = height;
            updateProjectionMatrix();
            updateViewMatrix();
        }

        protected Matrix4 m_Projection;
        protected Matrix4 m_View;
        protected Matrix4 m_VP;
        public Matrix4 getProjectionMatrix()
        {
            return m_Projection;
        }
        public Matrix4 getViewMatrix()
        {
            return m_View;
        }
        public Matrix4 getVPMatrix()
        {
            return m_VP;
        }
        protected void updateProjectionMatrix()
        {
            m_Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(getFOV()), (getFWidth() / getFHeight()), 0.1f, 100f);
            Matrix4 View = getViewMatrix();
            Matrix4 Proj = getProjectionMatrix();
            Matrix4 Projview;
            Matrix4.Mult(ref View, ref Proj, out Projview);
            m_VP = Projview;
        }
        protected virtual void updateViewMatrix()
        {
            m_View = Matrix4.LookAt(getPosition(), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));
            Matrix4 View = getViewMatrix();
            Matrix4 Proj = getProjectionMatrix();
            Matrix4 Projview;
            Matrix4.Mult(ref View, ref Proj, out Projview);
            m_VP = Projview;
        }

        protected float m_width;
        protected float m_height;
        protected float getFWidth()
        {
            return m_width;
        }
        protected float getFHeight()
        {
            return m_height;
        }
        public int getWidth()
        {
            return (int)m_width;
        }
        public int getHeight()
        {
            return (int)m_height;
        }
        public void setWidth(float width)
        {
            m_width = width;
            updateProjectionMatrix();
        }
        public void setHeight(float height)
        {
            m_height = height;
            updateProjectionMatrix();
        }

        protected int m_preferredFOV = 45;
        public int getPreferredFOV()
        {
            if (m_preferredFOV > 0 && m_preferredFOV <= 360)
            {
                return m_preferredFOV;
            }
            else
            {
                return 45;
            }
        }
        public void setPreferredFOV(int fov)
        {
            if (fov > 0 && fov <= 360)
            {
                m_preferredFOV = fov;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Can only set FOV between 0 and 360 degrees");
            }
        }

        protected int m_FOV = 0;
        public int getFOV()
        {
            if (m_FOV > 0 && m_FOV <= 360)
            {
                return m_FOV;
            }
            else
            {
                return getPreferredFOV();
            }
        }
        public void setFOV(int fov)
        {
            if (fov > 0 && fov <= 360)
            {
                m_FOV = fov;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Can only set FOV between 0 and 360 degrees");
            }
        }

        public override void setPosition(Vector3 pos)
        {
            base.setPosition(pos);
            updateViewMatrix();
        }
    }

    class OrbitCamera : Camera
    {
        private Vector3 pivot;

        private float rho; //radius
        private float theta; //azimuthal
        private float phi; //polar

        public OrbitCamera(float width, float height, int fov, Vector3 pivot) : base(width,height,fov)
        {
            this.pivot = pivot;
        }

        protected override void updateViewMatrix()
        {
            m_View = Matrix4.LookAt(getPosition(), pivot, new Vector3(0f, (phi > 0 ? 1f : -1f) * (Math.Abs(phi) < Math.PI ? 1f : -1f), 0f));
            Matrix4 View = getViewMatrix();
            Matrix4 Proj = getProjectionMatrix();
            Matrix4 Projview;
            Matrix4.Mult(ref View, ref Proj, out Projview);
            m_VP = Projview;
        }

        public override void setPosition(Vector3 pos)
        {
            rho = pos.Length;
            theta = (float)Math.Atan((pos.X - pivot.X) / (pos.Z - pivot.Z));
            phi = (float)Math.Acos((pos.Y - pivot.Y) / rho);
            base.setPosition(pos);
        }
        
        public void setPosition(Vector3 pos, bool recalculate)
        {
            if (!recalculate)
            {
                base.setPosition(pos);
                Console.WriteLine(new Vector4(theta / (float)(Math.PI), phi / (float)(Math.PI),getPosition().Z,getPosition().Y));

            }
            else
            {
                setPosition(pos);
            }
        }

        public void setPositionSpherical(Vector3 pos)
        {
            if (pos.Equals(getPositionSpherical())) { return; }

            float new_rho = pos.X;
            float new_theta = (pos.Y % (float)(2 * Math.PI)) + (float)(Math.Sign(pos.Y) < 0 ? 2*Math.PI : 0f);
            float new_phi = (pos.Z % (float)(2 * Math.PI)) + (float)(Math.Sign(pos.Y) < 0 ? 2 * Math.PI : 0f);

            rho = new_rho;
            theta = new_theta;
            phi = new_phi;

            //+(theta % (2*Math.PI))
                                                                //as Wolfram MathWorld sees it
            float x = rho * (float)Math.Sin(theta) * (float)Math.Sin(phi);  //y
            float y = rho * (float)Math.Cos(phi);                           //z
            float z = rho * (float)Math.Cos(theta) * (float)Math.Sin(phi);  //x

            setPosition(new Vector3(x+pivot.X,y+pivot.Y,z+pivot.Z),false);
        }

        public Vector3 getPositionSpherical() {

            return new Vector3(rho, theta, phi);
        }
    }
}
