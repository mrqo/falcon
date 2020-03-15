using Falcon.Editor.Components.Entities;
using Falcon.Engine.UI;
using ImGuiNET;
using Ninject;

namespace Falcon.Editor.Components
{
    public class App : Component
    {
        [Inject]
        public Entities.Inspector EntityInspector { get; set; }
        
        [Inject]
        public Systems.Inspector SystemInspector { get; set; }
        
        public override void Render()
        {
            ImGui.ShowDemoWindow();
            EntityInspector.Render();
            SystemInspector.Render();
        }
    }
}