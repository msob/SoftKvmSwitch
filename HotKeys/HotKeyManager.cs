using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace SoftKvmSwitch.HotKeys
{
    public class HotKeyManager : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class KeyCatchWindow : NativeWindow, IDisposable
        {
            private static readonly int WM_HOTKEY = 0x0312;

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public KeyCatchWindow()
            {
                CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Catch notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);

                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public void Dispose()
            {
                DestroyHandle();
            }
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private readonly KeyCatchWindow CatchingWindow = new();

        private int RegisteredHotKeyCount;

        public HotKeyManager()
        {
            CatchingWindow.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
            {
                KeyPressed?.Invoke(this, args);
            };
        }

        /// <summary>
        /// Registers a hot key.
        /// </summary>
        /// <param name="modifier">The modifiers associated with the hot key.</param>
        /// <param name="key">The key that is associated with the hot key.</param>
        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            RegisteredHotKeyCount++;

            if (!RegisterHotKey(CatchingWindow.Handle, RegisteredHotKeyCount, (uint)modifier, (uint)key))
            {
                throw new InvalidOperationException("Couldn’t register the hot key.");
            }
        }

        /// <summary>
        /// Registers a hot key.
        /// </summary>
        /// <param name="keyGesture">A string containing modifier and key names.</param>
        public void RegisterHotKey(string hotKeys)
        {
            string[] keys = hotKeys.Split("+", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            ModifierKeys modifier = 0;
            Keys key = Keys.None;

            foreach (string keyString in keys.ToList().GetRange(0, keys.Length - 1))
            {
                if (string.Compare("Alt", keyString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    modifier |= ModifierKeys.Alt;
                }
                else if (string.Compare("Control", keyString, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare("Steuerung", keyString, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare("Ctrl", keyString, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare("Strg", keyString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    modifier |= ModifierKeys.Control;
                }
                if (string.Compare("AltGr", keyString, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare("Alt Gr", keyString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    modifier |= ModifierKeys.Alt;
                    modifier |= ModifierKeys.Control;
                }
                else if (string.Compare("Shift", keyString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    modifier |= ModifierKeys.Shift;
                }
                else if (string.Compare("Win", keyString, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare("Windows", keyString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    modifier |= ModifierKeys.Win;
                }
                else
                {
                    throw new ArgumentException($"Unknown hot key modifier '{keyString}' found.");
                }
            }

            key = (Keys)Enum.Parse(typeof(Keys), keys.Last(), true);

            if (modifier != ModifierKeys.None && key != Keys.None)
            {
                RegisterHotKey(modifier, key);
            }
            else
            {
                throw new ArgumentException("Empty or unparsable hot key string.");
            }
        }

        public void Dispose()
        {
            for (int i = RegisteredHotKeyCount; i > 0; i--)
            {
                _ = UnregisterHotKey(CatchingWindow.Handle, i);
            }

            CatchingWindow.Dispose();
        }
    }
}
