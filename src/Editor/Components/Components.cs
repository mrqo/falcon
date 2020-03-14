using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.Ecs;
using Falcon.Engine.UI;
using ImGuiNET;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components
{
    public class EntityEdit : Component
    {
        public List<KeyValuePair<string, GameComponent>> ComponentViews { get; private set; } = new List<KeyValuePair<string, GameComponent>>();

        public void Init(Entity entity)
        {
            ComponentViews = entity.Components
                .Select(comp => new KeyValuePair<string, GameComponent>(comp.GetType().Name, GameComponent.Create(comp)))
                .ToList();
        }

        public override void Render()
        {
            foreach (var comp in ComponentViews)
            {
                if (ImGui.TreeNode(comp.Key))
                {
                    comp.Value.Render();
                    ImGui.TreePop();
                }
            }
        }
    }
}
