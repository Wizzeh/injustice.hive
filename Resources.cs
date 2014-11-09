using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Injustice_Hive
{
    class Resources
    {
        public static string LoadText(string path)
        {
            string output = "";

            using (StreamReader textLoader = new StreamReader(path))
            {
                do
                {
                    output += textLoader.ReadLine() + "\n";
                } while (textLoader.Peek() != -1);
            }
            return output;
        }

        public static float[] LoadMesh(string path)
        {
            float[] mesh;
            mesh = new float[3] { 1, 0, 2 };
            return mesh;
        }
    }
}
