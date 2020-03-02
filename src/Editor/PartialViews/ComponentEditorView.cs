using System;
using System.Collections.Generic;
using System.Text;
using Editor.Models;
using Engine.UI;
using ImGuiNET;

namespace Editor.PartialViews
{
    public class ComponentEditorView : View
    {
        public delegate void OnPropertyChanged(string propertyName, object newValue);

        public OnPropertyChanged OnPropertyChangedHandler { get; set; }

        public string ComponentName { get; set; }

        public List<EditorProperty> Properties { get; set; } = new List<EditorProperty>();

        public override void Render()
        {
            ImGui.Text(ComponentName);
            for (int i = 0; i < Properties.Count; i++)
            {
                if (Properties[i].PropertyType == typeof(double))
                {
                    double a = (double)Properties[i].Value;
                    ImGui.InputDouble(Properties[i].Name, ref a, 0.01);
                }

                if (Properties[i].PropertyType == typeof(int))
                {
                    int a = (int) Properties[i].Value;
                    ImGui.InputInt(Properties[i].Name, ref a, 1);
                }
            }
        }
    }
}
