using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Injustice_Hive
{
    interface IEntity
    {
        Vector3 getPosition();
        void setPosition(Vector3 pos);

        Quaternion getRotation();
        void setRotation(Quaternion rot);
        void normalizeRotation();

        Scene getScene();
        void addedToScene(Scene scene);
    }
}
