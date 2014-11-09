using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;


namespace Injustice_Hive
{
    /* More Additions to InjusticeHive can be found in the following files:
     * OnLoad.cs
     */
    partial class InjusticeHive : GameWindow
    {

        /*
         * Internal stopwatches for keeping track of timesteps
         * ---------------------------------------------------
         * Apparently these are the most accurate timers in C#
         * despite the fact that they're in System.Diagnostics
         */
        private System.Diagnostics.Stopwatch renderTimer = new System.Diagnostics.Stopwatch();
        private System.Diagnostics.Stopwatch logicTimer = new System.Diagnostics.Stopwatch();

        /*                    Camera Stuff 
         * Might need to be moved to another class eventually
         */
        //public Vector3 camerapos = new Vector3(4f, 3f, 3f);

        /* Actual relevant class properties */
        private Scene m_currentScene;
        public Scene getCurrentScene() { return m_currentScene; }
        public void setCurrentScene(Scene scene) { m_currentScene = scene; }

        //A bunch of shader related IDS
        //ID of the vertex buffer
        //static int vertexBufferID;
        //ID of the color buffer
        //static int colorBufferID;

        //ID of the MVP shader memory thing
        public static int mvpID;
        //ID of the program
        public static int programID;

        public InjusticeHive(int width, int height, OpenTK.Graphics.GraphicsMode gmode, string title)
            : base(width, height, gmode, title)
        {
            //Any extra constructor stuff will go here. 
            //GameWindow has other constructors
            //but I don't need them right now
        }

        [STAThread]
        public static int Main()
        {
            Dictionary<String, int> dict = new Dictionary<String, int>();
            //dict["hello"] = 5;
            //Console.WriteLine(dict["hello"]);
            //dict.Remove("goodby");
            //dict.Remove("hello");
            //Console.WriteLine(dict["hello"]);
            //I would probably read from an INI file here, but for now I'm just prebaking the settings.
            WindowData settings = new WindowData { X = 0,
                Y = 0,
                Height = 1000,
                Width = 1000,
                Title="Injustice Hive",
                FOV = 45};

            //This functions as its own cool little try.. finally dispose block
            using (InjusticeHive game = 
                new InjusticeHive(
                    settings.Width, 
                    settings.Height, 
                    OpenTK.Graphics.GraphicsMode.Default, 
                    settings.Title))
            {
                game.KeyPress += new EventHandler<KeyPressEventArgs>(game_KeyPress);
                game.Run();
            }
            return 0;
        }

        static void game_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(e.KeyChar);
        }
    }
}
