using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Injustice_Hive
{
    class Entity : IEntity
    {
        public Quaternion getRotation()
        {
            throw new NotImplementedException();
        }

        public void setRotation(Quaternion rot)
        {
            throw new NotImplementedException();
        }

        public void normalizeRotation()
        {
            throw new NotImplementedException();
        }

        private Vector3 m_Position;
        public Vector3 getPosition()
        {
            return m_Position;
        }
        public virtual void setPosition(Vector3 pos)
        {
            m_Position = pos;
        }

        private Scene m_scene;
        public Scene getScene()
        {
            return m_scene;
        }

        public void setScene(Scene scene)
        {
            m_scene = scene;
            scene.objectAdded(this);
        }

        public void addedToScene(Scene scene)
        {
            m_scene = scene;
        }
    }
}
