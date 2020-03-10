using System;
using System.Numerics;
using Falcon.Engine;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Implementation.EntityComponentModel;
using Falcon.Engine.Execution;
using Falcon.Game;
using Falcon.Editor.Views;
using Falcon.Editor.Controllers;
using Falcon.Engine.Communication;
using Falcon.Engine.Implementation.Execution;
using Falcon.Engine.Implementation.Networking;
using Falcon.Engine.Networking;
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

        private static EntitiesView _entitiesView;
        private static EntitiesController _entitiesController;

        static void Main(string[] args)
        {
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "ECS"), 
                new GraphicsDeviceOptions(true, null, true), 
                out _window,
                out _gd);

            IKernel kernel = new StandardKernel();

            ConfigureDependencies(kernel);

            var executionEnv = new Executor(kernel);

            _cl = _gd.ResourceFactory.CreateCommandList();

            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);
            _entitiesView = new EntitiesView();
            _entitiesController = new EntitiesController(_entitiesView, kernel.Get<IExecutionTarget>() as IEntityProvider);

            while (_window.Exists)
            {
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists)
                {
                    break;
                }
                _controller.Update(1f / 60f, snapshot);

                //SubmitUi();
                _entitiesView.Render();
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
                .Bind<IExecutionTarget>()
                .To<Falcon.Game.Game>()
                .InSingletonScope();

            kernel
                .Bind<INotificationHub>()
                .To<NotificationHub>()
                .InSingletonScope();

            kernel
                .Bind<IComponentFactory>()
                .To<ComponentFactory>()
                .InSingletonScope();

            kernel
                .Bind<IComponentResolverFactory>()
                .To<ComponentResolverFactory>()
                .InSingletonScope();

            kernel
                .Bind<IStateManager>()
                .To<StateManager>();
        }

        private static unsafe void SubmitUi()
        {
            bool show = true;
            ImGui.Begin("Another Window", ref show);
            ImGui.Text("Hello from another window!");
            ImGui.Button("Close Me");
            ImGui.Text($"Mouse position: {ImGui.GetMousePos()}");
            ImGui.End();

            if (ImGui.TreeNode("Tabs"))
            {
                if (ImGui.TreeNode("Basic"))
                {
                    ImGuiTabBarFlags tab_bar_flags = ImGuiTabBarFlags.None;
                    if (ImGui.BeginTabBar("MyTabBar", tab_bar_flags))
                    {
                        if (ImGui.BeginTabItem("Avocado"))
                        {
                            ImGui.Text("This is the Avocado tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Broccoli"))
                        {
                            ImGui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Cucumber"))
                        {
                            ImGui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        ImGui.EndTabBar();
                    }
                    ImGui.Separator();
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Advanced & Close Button"))
                {
                    // Expose a couple of the available flags. In most cases you may just call BeginTabBar() with no flags (0).
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_Reorderable", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.Reorderable);
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_AutoSelectNewTabs", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.AutoSelectNewTabs);
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_NoCloseWithMiddleMouseButton", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.NoCloseWithMiddleMouseButton);
                    if ((s_tab_bar_flags & (uint)ImGuiTabBarFlags.FittingPolicyMask) == 0)
                        s_tab_bar_flags |= (uint)ImGuiTabBarFlags.FittingPolicyDefault;
                    if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyResizeDown", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.FittingPolicyResizeDown))
                        s_tab_bar_flags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyResizeDown);
                    if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.FittingPolicyScroll))
                        s_tab_bar_flags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyScroll);

                    // Tab Bar
                    string[] names = { "Artichoke", "Beetroot", "Celery", "Daikon" };

                    for (int n = 0; n < s_opened.Length; n++)
                    {
                        if (n > 0) { ImGui.SameLine(); }
                        ImGui.Checkbox(names[n], ref s_opened[n]);
                    }

                    // Passing a bool* to BeginTabItem() is similar to passing one to Begin(): the underlying bool will be set to false when the tab is closed.
                    if (ImGui.BeginTabBar("MyTabBar", (ImGuiTabBarFlags)s_tab_bar_flags))
                    {
                        for (int n = 0; n < s_opened.Length; n++)
                            if (s_opened[n] && ImGui.BeginTabItem(names[n], ref s_opened[n]))
                            {
                                ImGui.Text($"This is the {names[n]} tab!");
                                if ((n & 1) != 0)
                                    ImGui.Text("I am an odd tab.");
                                ImGui.EndTabItem();
                            }
                        ImGui.EndTabBar();
                    }
                    ImGui.Separator();
                    ImGui.TreePop();
                }
                ImGui.TreePop();
            }
        }
    }
}
