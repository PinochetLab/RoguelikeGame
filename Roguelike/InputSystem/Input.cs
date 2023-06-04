using Microsoft.Xna.Framework.Input;
using System;
using Roguelike.VectorUtility;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Xna.Framework;

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

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();


        private static IntPtr GetActiveWindow()
        {
            IntPtr handle = IntPtr.Zero;
            return GetForegroundWindow();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static Vector2Int GetWindowCoordinate()
        {
            if (GetWindowRect(GetActiveWindow(), out RECT r))
            {
                return new Vector2Int(r.Left, r.Top);
            }
            return Vector2Int.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner  
            public int Top;         // y position of upper-left corner  
            public int Right;       // x position of lower-right corner  
            public int Bottom;      // y position of lower-right corner  
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Vector2Int(POINT point)
            {
                return new Vector2(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Vector2Int GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint - GetWindowCoordinate() - new Vector2Int(8, 36);
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
