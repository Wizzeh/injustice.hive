using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Injustice_Hive
{
    interface IRenderableEntity : IEntity, IDrawable
    {
        int getVertexHandle();
        int getIndexHandle();
    }
}
