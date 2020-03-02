using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Editor.Controllers;
using Falcon.Editor.PartialViews;
using Falcon.Engine.UI;
using ImGuiNET;

namespace Falcon.Editor.Views
{
    public class EntitiesView : View
    {
        protected int _currentEntitySelectId;

        public int SelectedEntityId => _currentEntitySelectId;

        public IEnumerable<string> Entities { get; set; }

        public delegate void OnCreate();

        public OnCreate OnCreateEventHandler { get; set; }

        public EntityEditorView EntityEditorView = new EntityEditorView();

        public override void Render()
        {
            ImGui.Begin("Creation");
            ImGui.ListBox(string.Empty, ref _currentEntitySelectId, Entities.ToArray(), Entities.Count());
            if (ImGui.Button("Create"))
            {
                OnCreateEventHandler?.Invoke();
            }

            EntityEditorView.Render();

            ImGui.End();
        }
    }
}
