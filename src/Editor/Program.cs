using System;
using System.Numerics;
using Falcon.Engine;
using Falcon.Engine.Ecs;
using Falcon.Engine.Implementation.Ecs;
using Falcon.Engine.Execution;
using Falcon.Game;
using Falcon.Editor.Components;
using Falcon.Editor.Components.Entities;
using Falcon.Editor.Components.Systems;
using Falcon.Engine.Communication;
using Falcon.Engine.Implementation.Communication;
using Falcon.Engine.Implementation.Execution;
using Falcon.Engine.Implementation.Networking;
using Falcon.Engine.Networking;
using Falcon.Game.Systems;
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

        static void Main(string[] args)
        {
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "Falcon Editor"), 
                new GraphicsDeviceOptions(true, null, true), 
                out _window,
                out _gd);

            IKernel kernel = new StandardKernel();

            ConfigureDependencies(kernel);

            var executionEnv = kernel.Get<IExecutor>();
            var app = kernel.Get<App>();
            
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);
            
            
            while (_window.Exists)
            {
                var snapshot = _window.PumpEvents();

                _controller.Update(1f / 60f, snapshot);
                
                executionEnv.Step(1f / 60f);
                app.Render();
                
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
	        ConfigureEditor(kernel);
	        ConfigureEngine(kernel);
	        ConfigureGame(kernel);
        }

        private static void ConfigureEditor(IKernel kernel)
        {
	        kernel
		        .Bind<App>()
		        .ToSelf();

	        kernel
		        .Bind<ComponentsEdit>()
		        .ToSelf();

	        kernel
		        .Bind<ComponentEdit>()
		        .ToSelf();

	        kernel
		        .Bind<Falcon.Editor.Components.Entities.Inspector>()
		        .ToSelf();

	        kernel
		        .Bind<PropertyList>()
		        .ToSelf();

	        kernel
		        .Bind<Falcon.Editor.Components.Systems.Inspector>()
		        .ToSelf();
        }
        
        private static void ConfigureEngine(IKernel kernel)
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
		        .Bind<IBootstrapper>()
		        .To<Falcon.Game.Bootstrapper>()
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
		        .Bind<ISystemManager>()
		        .To<SystemManager>()
		        .InSingletonScope();
        }

        private static void ConfigureGame(IKernel kernel)
        {
	        kernel
		        .Bind<ISystem>()
		        .To<PlayerMovementSystem>()
		        .InSingletonScope();
        }
    }
}
