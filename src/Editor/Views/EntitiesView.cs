using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.UI;
using ImGuiNET;

namespace Falcon.Editor.Views
{
    public class EntitiesView : View
    {
        private EntityEditView entityEditView = new EntityEditView();

        private IEntityProvider entityProvider;

        protected int selectedEntityId;

        public static EntitiesView Create(IEntityProvider entityProvider)
        {
            var view = new EntitiesView();
            view.Init(entityProvider);
            return view;
        }

        public void Init(IEntityProvider entityProvider)
        {
            this.entityProvider = entityProvider;
            this.entityEditView.Init(entityProvider.Entities.FirstOrDefault());
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

            entityEditView.Render();

            ImGui.End();
        }

        private void OnEditPressed()
        {
            Console.WriteLine($"Edit entity {selectedEntityId}");
            this.entityEditView.Init(entityProvider.Entities.ToArray()[selectedEntityId]);
        }
    }
}
