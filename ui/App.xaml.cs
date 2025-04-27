using Microsoft.Maui.Controls;
#if WINDOWS
using System.Runtime.InteropServices;
#endif
namespace ui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
#if WINDOWS
            window.HandlerChanged += (s, e) =>
            {
                if (window.Handler.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
                {
                    IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                    ShowWindow(hWnd, 3);
                }
            };
#endif
            return window;
        }

#if WINDOWS
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
#endif
    }
}
