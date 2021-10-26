using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ArtilleryCalculator
{
    class TransparencyManager
    {
        const int TRANSPARENCY_FLAGS = Native.WS_EX_LAYERED | Native.WS_EX_TRANSPARENT;

        Form Form { get; }
        Size OriginalSize { get; }
        Size TransparentSize { get; }

        public TransparencyManager(Form form)
        {
            Form = form;
            OriginalSize = form.ClientSize;
            TransparentSize = new Size(form.ClientSize.Width - 115, form.ClientSize.Height);
            Form.Paint += Form_Paint;
        }

        private bool _enableTransparency = false;
        public bool EnableTransparency
        {
            get => _enableTransparency;
            set
            {
                _enableTransparency = value;

                if (_enableTransparency)
                {
                    Form.ClientSize = TransparentSize;
                    Form.FormBorderStyle = FormBorderStyle.None;
                    Form.Opacity = 0.9;
                }
                else if (!_enableTransparency)
                {
                    Form.ClientSize = OriginalSize;
                    Form.FormBorderStyle = FormBorderStyle.FixedSingle;
                    Form.Opacity = 1.0;
                }

                Form.Invalidate();
            }
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            int style = Native.GetWindowLong(Form.Handle, Native.GWL_EXSTYLE);
            int newStyle = EnableTransparency ? (style | TRANSPARENCY_FLAGS) : (style & ~TRANSPARENCY_FLAGS);
            Native.SetWindowLong(Form.Handle, Native.GWL_EXSTYLE, newStyle);
        }

        static class Native
        {
            public const int GWL_EXSTYLE = -20;
            public const int WS_EX_LAYERED = 0x80000;
            public const int WS_EX_TRANSPARENT = 0x20;

            [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        }
    }
}
