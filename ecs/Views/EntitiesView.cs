using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Editor.Controllers;
using Editor.PartialViews;
using Engine.UI;
using ImGuiNET;

namespace Editor.Views
{
    public class EntitiesView : View
    {
        protected int _currentEntitySelectId;

        public int SelectedEntityId => _currentEntitySelectId;

        public IEnumerable<string> Entities { get; set; }

        public delegate void OnCreate();

        public OnCreate OnCreateEventHandler { get; set; }

        public ComponentEditorView CompEditView = new ComponentEditorView();

        protected ComponentEditorController _compEditController;

        public override void Render()
        {
            ImGui.Begin("Creation");
            ImGui.ListBox(string.Empty, ref _currentEntitySelectId, Entities.ToArray(), Entities.Count());
            if (ImGui.Button("Create"))
            {
                OnCreateEventHandler?.Invoke();
            }

            ImGui.Text("Components preview");

            CompEditView.Render();

            ImGui.End();
        }
    }
}
