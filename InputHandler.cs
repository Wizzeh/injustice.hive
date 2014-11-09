using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Input;

namespace Injustice_Hive
{
    class InputHandler
    {
        private KeyboardState prevState;
        private Dictionary<Key[],int> keysTracking;

        public InputHandler()
        {
            prevState = Keyboard.GetState();
            
        }

        public void registerKey(Key[] keyCombo, int function)
        {
            keysTracking[keyCombo] = function;
        }

        public void unregisterKey(Key[] keyCombo)
        {
            if(keysTracking.ContainsKey(keyCombo))
            {
                keysTracking.Remove(keyCombo);
            }
        }

    }
}
