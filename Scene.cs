using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Injustice_Hive
{
    class Scene : IDrawable
    {
        private List<IRenderableEntity> m_entities = new List<IRenderableEntity>();

        private Camera m_camera;
        public Camera getCamera() { return m_camera; }
        public void setCamera(Camera camera)
        {
            m_camera = camera;
            m_camera.setScene(this);
        }

        public void objectAdded(Camera camera)
        {
            m_camera = camera;
        }

        public void objectAdded(GameObject obj)
        {

        }

        public void objectAdded(Entity entity)
        {

        }

        public void addObject(GameObject obj)
        {
            m_entities.Add(obj);
            obj.addedToScene(this);
        }

        

        public void draw(float deltaTime)
        {
            foreach (IRenderableEntity entity in m_entities)
            {
                entity.draw(deltaTime);
            }
        }

        public void init()
        {
            addGODefault();
        }

        private void addGODefault()
        {
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(i);
                addObject(new GameObject(rnd.Next(-2,3),rnd.Next(-2,3),rnd.Next(-2,3)));
            }
        }

    }
    
}
