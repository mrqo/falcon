using Falcon.Engine.Ecs;
using ImGuiNET;
using Ninject.Infrastructure.Language;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components.Systems
{
    public class Inspector : Component
    {
        private ISystemManager _systemManager;
        
        public Inspector(ISystemManager systemManager)
        {
            _systemManager = systemManager;
        }
        
        public override void Render()
        {
            ImGui.Begin("Systems");
            
            _systemManager.Systems.Map(sys =>
            {
                bool isActive = sys.IsActive;
                ImGui.Checkbox(sys.GetType().FullName, ref isActive);
                sys.IsActive = isActive;
            });

            ImGui.End();
        }
    }
}