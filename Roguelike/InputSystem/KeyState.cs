using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    public class KeyState
    {
        private bool pressed = true;
        private bool lastPressed = false;

        public KeyState() { }

        public bool Pressed
        {
            get => pressed;
            set
            {
                lastPressed = pressed;
                pressed = value;
            }
        }

        public bool IsDown { get => pressed && !lastPressed; }

        public bool IsUp { get => !pressed && lastPressed; }
    }
}
