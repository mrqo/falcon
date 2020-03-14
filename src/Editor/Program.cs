using System;
using System.Numerics;
using Falcon.Engine;
using Falcon.Engine.Ecs;
using Falcon.Engine.Implementation.Ecs;
using Falcon.Engine.Execution;
using Falcon.Game;
using Falcon.Editor.Components;
using Falcon.Engine.Communication;
using Falcon.Engine.Implementation.Communication;
using Falcon.Engine.Implementation.Execution;
using Falcon.Engine.Implementation.Networking;
using Falcon.Engine.Networking;
using Falcon.Game.Systems;
using ImGuiNET;
using Ninject;
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
        private static uint s_tab_bar_flags = (uint)ImGuiTabBarFlags.Reorderable;
        static bool[] s_opened = { true, true, true, true };

        private static ImGuiController _controller;

        private static Entities _entitiesComponent;

        static void Main(string[] args)
        {
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Maximized, "Falcon Editor"), 
                new GraphicsDeviceOptions(true, null, true), 
                out _window,
                out _gd);

            IKernel kernel = new StandardKernel();

            ConfigureDependencies(kernel);

            var executionEnv = kernel.Get<IExecutor>();

            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);
            _entitiesComponent = Entities.Create(kernel.Get<IEntityProvider>());
            
            
            Style();
            while (_window.Exists)
            {
                InputSnapshot snapshot = _window.PumpEvents();

                _controller.Update(1f / 60f, snapshot);
                _entitiesComponent.Render();
                executionEnv.Step(1f / 60f);

                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
                _controller.Render(_gd, _cl);
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }

            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }

        private static void ConfigureDependencies(IKernel kernel)
        {
            kernel
                .Bind<IServerStub>()
                .To<ServerStub>()
                .InSingletonScope();

            kernel
                .Bind<IExecutor>()
                .To<Executor>()
                .InSingletonScope();

            kernel
                .Bind<IEntityProvider>()
                .To<EntityProvider>()
                .InSingletonScope();

            kernel
                .Bind<IEntityQueryBuilder>()
                .To<EntityQueryBuilder>();
            
            kernel
                .Bind<IExecutionTarget>()
                .To<Falcon.Game.Game>()
                .InSingletonScope();

            kernel
                .Bind<INotificationHub>()
                .To<NotificationHub>()
                .InSingletonScope();

            kernel
                .Bind<IComponentFactory>()
                .To<ComponentFactory>();

            kernel
                .Bind<IComponentResolver>()
                .To<ComponentResolver>();

            kernel
                .Bind<IStateManager>()
                .To<StateManager>();

            kernel
                .Bind<IEntityFactory>()
                .To<EntityFactory>()
                .InSingletonScope();

            kernel
                .Bind<EntityDeps>()
                .ToSelf();

            kernel
	            .Bind<PlayerMovementSystem>()
	            .ToSelf();
        }
        
        private static void Style()
        {
	        /*
	        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.00f, 1.00f, 1.00f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.TextDisabled, new Vector4(0.40f, 0.40f, 0.40f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.25f, 0.25f, 0.25f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(50.0f / 255, 50.0f / 255, 50.0f / 255, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.25f, 0.25f, 0.25f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.Border, new Vector4(0.12f, 0.12f, 0.12f, 0.71f));
			ImGui.PushStyleColor(ImGuiCol.FrameBg, new Vector4(0.42f, 0.42f, 0.42f, 0.54f));
			ImGui.PushStyleColor(ImGuiCol.BorderShadow, new Vector4(1.00f, 1.00f, 1.00f, 0.06f));
			ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, new Vector4(0.42f, 0.42f, 0.42f, 0.40f));
			ImGui.PushStyleColor(ImGuiCol.FrameBgActive, new Vector4(0.56f, 0.56f, 0.56f, 0.67f));
			ImGui.PushStyleColor(ImGuiCol.TitleBg, new Vector4(0.19f, 0.19f, 0.19f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.TitleBgActive, new Vector4(0.22f, 0.22f, 0.22f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.TitleBgCollapsed, new Vector4(0.17f, 0.17f, 0.17f, 0.90f));
			ImGui.PushStyleColor(ImGuiCol.MenuBarBg, new Vector4(0.335f, 0.335f, 0.335f, 1.000f));
			ImGui.PushStyleColor(ImGuiCol.ScrollbarBg, new Vector4(0.24f, 0.24f, 0.24f, 0.53f));
			ImGui.PushStyleColor(ImGuiCol.ScrollbarGrab, new Vector4(0.41f, 0.41f, 0.41f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.ScrollbarGrabHovered, new Vector4(0.52f, 0.52f, 0.52f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.ScrollbarGrabActive, new Vector4(0.76f, 0.76f, 0.76f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.CheckMark, new Vector4(0.65f, 0.65f, 0.65f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.SliderGrab, new Vector4(0.52f, 0.52f, 0.52f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.SliderGrabActive, new Vector4(0.64f, 0.64f, 0.64f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.54f, 0.54f, 0.54f, 0.35f));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.52f, 0.52f, 0.52f, 0.59f));
			ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.76f, 0.76f, 0.76f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0.38f, 0.38f, 0.38f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(0.47f, 0.47f, 0.47f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0.76f, 0.76f, 0.76f, 0.77f));
			ImGui.PushStyleColor(ImGuiCol.Separator, new Vector4(0.000f, 0.000f, 0.000f, 0.137f));
			ImGui.PushStyleColor(ImGuiCol.SeparatorHovered, new Vector4(0.700f, 0.671f, 0.600f, 0.290f));
			ImGui.PushStyleColor(ImGuiCol.SeparatorActive, new Vector4(0.702f, 0.671f, 0.600f, 0.674f));
			ImGui.PushStyleColor(ImGuiCol.ResizeGrip, new Vector4(0.26f, 0.59f, 0.98f, 0.25f));
			ImGui.PushStyleColor(ImGuiCol.ResizeGripHovered, new Vector4(0.26f, 0.59f, 0.98f, 0.67f));
			ImGui.PushStyleColor(ImGuiCol.ResizeGripActive, new Vector4(0.26f, 0.59f, 0.98f, 0.95f));
			ImGui.PushStyleColor(ImGuiCol.PlotLines, new Vector4(0.61f, 0.61f, 0.61f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.PlotLinesHovered, new Vector4(1.00f, 0.43f, 0.35f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, new Vector4(0.90f, 0.70f, 0.00f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.PlotHistogramHovered, new Vector4(1.00f, 0.60f, 0.00f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.TextSelectedBg, new Vector4(0.73f, 0.73f, 0.73f, 0.35f));
			ImGui.PushStyleColor(ImGuiCol.ModalWindowDimBg, new Vector4(0.80f, 0.80f, 0.80f, 0.35f));
			ImGui.PushStyleColor(ImGuiCol.DragDropTarget, new Vector4(1.00f, 1.00f, 0.00f, 0.90f));
			ImGui.PushStyleColor(ImGuiCol.NavHighlight, new Vector4(0.26f, 0.59f, 0.98f, 1.00f));
			ImGui.PushStyleColor(ImGuiCol.NavWindowingHighlight, new Vector4(1.00f, 1.00f, 1.00f, 0.70f));
			ImGui.PushStyleColor(ImGuiCol.NavWindowingDimBg, new Vector4(0.80f, 0.80f, 0.80f, 0.20f));
			*/
	        
			ImGui.StyleColorsLight();
			
			ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.PopupRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, 0);
			ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarSize, 14);
			ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize,1);
			ImGui.PushStyleVar(ImGuiStyleVar.ChildBorderSize,1);
			ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize,1);
			ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize,0);
			ImGui.PushStyleVar(ImGuiStyleVar.IndentSpacing, 20);
			ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(4, 4));
			ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(8, 3));
			ImGui.PushStyleVar(ImGuiStyleVar.ItemInnerSpacing, new Vector2(6, 2));
			ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(7, 7));
			ImGui.PushStyleVar(ImGuiStyleVar.WindowTitleAlign, new Vector2(0.5f, 0.5f)); 
        }
    }
}
