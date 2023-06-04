using Microsoft.Xna.Framework.Input;
using System;
using Roguelike.VectorUtility;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace Roguelike;

public static class Input
{
    private static readonly Dictionary<Keys, KeyState> States = new();

    public static bool IsPressed(Keys key) => States.TryGetValue(key, out var state) && state.Pressed;

    public static bool IsDown(Keys key) => States.TryGetValue(key, out var state) && state.IsDown;

    public static bool IsUp(Keys key) => States.TryGetValue(key, out var state) && state.IsUp;

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();


    private static IntPtr GetActiveWindow() => GetForegroundWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RectangleInternal rectangle);

    public static Vector2Int GetWindowCoordinate() =>
        GetWindowRect(GetActiveWindow(), out var r)
            ? new Vector2Int(r.Left, r.Top)
            : Vector2Int.Zero;

    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleInternal
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PointInternal
    {
        public int X;
        public int Y;

        public static implicit operator Vector2Int(PointInternal pointInternal) => new(pointInternal.X, pointInternal.Y);
    }

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out PointInternal lpPointInternal);

    public static Vector2Int GetCursorPosition()
    {
        GetCursorPos(out var lpPointInternal);

        return lpPointInternal - GetWindowCoordinate() - new Vector2Int(8, 36);
    }

    public static void Update()
    {
        var pressedKeys = Keyboard.GetState().GetPressedKeys();

        foreach (var item in States.Where(item => !pressedKeys.Contains(item.Key)))
            item.Value.Pressed = false;

        foreach (var key in pressedKeys)
        {
            if (States.TryGetValue(key, out var state))
                state.Pressed = true;
            else
                States.Add(key, new KeyState());
        }
    }
}