using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Engine.Ecs;
using Falcon.Engine.UI;
using Falcon.Game.Components;
using ImGuiNET;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components
{
    public class Inspector : Component
    {
        private ComponentsEdit _componentsEdit;
        
        public Inspector(
            ComponentsEdit componentsEdit,
            IEntityProvider entityProvider)
        {
            _componentsEdit = componentsEdit;

            componentsEdit.EntityId = entityProvider
                .Query()
                .With<JumpComponent>()
                .With<WalkComponent>()
                .Get()
                .FirstOrDefault()
                ?.GetHashCode() ?? -1;
        }
        
        public override void Render()
        {
            ImGui.Begin("Inspector");
            
            if (ImGui.BeginTabBar("entityTabs"))
            {
                if (ImGui.BeginTabItem("Components"))
                {
                    _componentsEdit.Render();
                    
                    if (ImGui.Button("Add component"))
                    {
                        OnAddComponentPressed();
                    }
                    
                    ImGui.SameLine();
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Properties"))
                {
                    ImGui.EndTabItem();
                }
            }
            
            ImGui.End();
        }
        
        private void OnAddComponentPressed()
        {
        }
    }
}
