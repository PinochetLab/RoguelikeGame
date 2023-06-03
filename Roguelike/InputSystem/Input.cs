using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    public static class Input
    {

        private static Dictionary<Keys, KeyState> states = new();

        public static bool IsPressed(Keys key)
        {
            if (states.TryGetValue(key, out var state))
            {
                return state.Pressed;
            }
            return false;
        }

        public static bool IsDown(Keys key)
        {
            if (states.TryGetValue(key, out var state))
            {
                return state.IsDown;
            }
            return false;
        }

        public static bool IsUp(Keys key)
        {
            if (states.TryGetValue(key, out var state))
            {
                return state.IsUp;
            }
            return false;
        }

        public static void Update()
        {
            var pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (var item in states)
            {
                if (!pressedKeys.Contains(item.Key))
                {
                    item.Value.Pressed = false;
                }
            }

            foreach (var key in pressedKeys)
            {
                if (states.ContainsKey(key))
                {
                    states[key].Pressed = true;
                }
                else
                {
                    states.Add(key, new KeyState());
                }
            }
        }
    }
}
