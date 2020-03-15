using Falcon.Engine.UI;
using ImGuiNET;

namespace Falcon.Editor.Components
{
    public class App : Component
    {
        private readonly Inspector _inspector;
        
        public App(Inspector inspector)
        {
            _inspector = inspector;
        }
        
        public override void Render()
        {
            ImGui.ShowDemoWindow();
            _inspector.Render();
        }
    }
}