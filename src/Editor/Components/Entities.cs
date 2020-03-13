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
        private EntityEdit _entityEditComponent = new EntityEdit();

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
            
            this._entityEditComponent.Init(entity);
        }

        public override void Render()
        {
            ImGui.Begin("Creation");
            ImGui.ListBox(
                string.Empty, 
                ref selectedEntityId, 
                entityProvider
                        .Entities
                        .Select(entity => entity.GetType().Name)
                        .ToArray(),
                entityProvider
                    .Entities.Count());

            if (ImGui.Button("Edit"))
            {
                OnEditPressed();
            }

            _entityEditComponent.Render();

            ImGui.End();
        }

        private void OnEditPressed()
        {
            Console.WriteLine($"Edit entity {selectedEntityId}");
            this._entityEditComponent.Init(entityProvider.Entities.ToArray()[selectedEntityId]);
        }
    }
}
