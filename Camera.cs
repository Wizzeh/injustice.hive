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
            setPosition(new Vector3(0, 0, 0));
            m_Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (width / height), 0.1f, 100f);
            m_View = Matrix4.LookAt(getPosition(), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));
        }

        private Matrix4 m_Projection;
        private Matrix4 m_View;
        private Matrix4 m_VP;
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
        private void updateProjectionMatrix()
        {
            m_Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(getFOV()), (getFWidth() / getFHeight()), 0.1f, 100f);
            Matrix4 View = getViewMatrix();
            Matrix4 Proj = getProjectionMatrix();
            Matrix4 Projview;
            Matrix4.Mult(ref View, ref Proj, out Projview);
            m_VP = Projview;
        }
        private void updateViewMatrix()
        {
            m_View = Matrix4.LookAt(getPosition(), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));
            Matrix4 View = getViewMatrix();
            Matrix4 Proj = getProjectionMatrix();
            Matrix4 Projview;
            Matrix4.Mult(ref View, ref Proj, out Projview);
            m_VP = Projview;
        }

        private float m_width;
        private float m_height;
        private float getFWidth()
        {
            return m_width;
        }
        private float getFHeight()
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

        private int m_preferredFOV = 45;
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

        private int m_FOV = 0;
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

        public void setPosition(Vector3 pos)
        {
            base.setPosition(pos);
            updateViewMatrix();
        }
    }
}
