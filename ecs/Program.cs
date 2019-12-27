using System;
using System.Numerics;
using Engine;
using Engine.EntityComponentModel;
using Engine.Implementation.EntityComponentModel;
using Engine.Execution;
using Game;
using ImGuiNET;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

using static ImGuiNET.ImGuiNative;

namespace ImGuiNET
{
    class Program
    {
        private static Sdl2Window _window;
        private static GraphicsDevice _gd;
        private static CommandList _cl;

        private static Vector3 _clearColor = new Vector3(0.45f, 0.55f, 0.6f);

        static void Main(string[] args)
        {
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "ECS"), 
                new GraphicsDeviceOptions(true, null, true), 
                out _window,
                out _gd);

            var game = new Executor()
                .SetTarget(new Game.Game())
                .SetNotificationHub(new NotificationHub())
                .SetComponentFactory(new ComponentFactory())
                .SetComponentResolverFactory(new ComponentResolverFactory())
                .Init();

            _cl = _gd.ResourceFactory.CreateCommandList();

            // #TODO: Rest of initialization
            // https://github.com/mellinoe/ImGui.NET/blob/master/src/ImGui.NET.SampleProgram/ImGuiController.cs

            IntPtr context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);
            
            var fonts = ImGui.GetIO().Fonts;
            ImGui.GetIO().Fonts.AddFontDefault();

            ImGui.NewFrame();

            while (_window.Exists)
            {
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists)
                {
                    break;
                }

                SubmitUi();
                game.Step(ImGui.GetIO().Framerate);

                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }

            _cl.Dispose();
            _gd.Dispose();
        }

        private static unsafe void SubmitUi()
        {
            ImGui.Begin("Another Window");
            ImGui.Text("Hello from another window!");
            ImGui.Button("Close Me");
            ImGui.End();
        }
    }
}
