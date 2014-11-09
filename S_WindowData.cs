using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Math;
using System.Drawing;
using OpenTK;
using OpenTK.Input;

namespace Injustice_Hive
{
    class WindowData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool Fullscreen { get; set; }
        public string Title { get; set; }

        public int FOV
        {
            get;
            set;
        }
    }
}
