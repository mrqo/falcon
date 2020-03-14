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
    public class Entities : Component
    {
        private EntityEdit _components = new EntityEdit();

        private IEntityProvider entityProvider;

        protected int selectedEntityId;

        public static Entities Create(IEntityProvider entityProvider)
        {
            var view = new Entities();
            view.Init(entityProvider);
            return view;
        }

        public void Init(IEntityProvider entityProvider)
        {
            this.entityProvider = entityProvider;
            
            var entity = entityProvider
                .Query()
                .With<JumpComponent>()
                .With<WalkComponent>()
                .Get()
                .FirstOrDefault();
            
            this._components.Init(entity);
        }

        public override void Render()
        {
            ImGui.ShowDemoWindow();
            ImGui.Begin("Inspector");
            
            if (ImGui.BeginTabBar("entityTabs"))
            {
                if (ImGui.BeginTabItem("Components"))
                {
                    _components.Render();
                    
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
