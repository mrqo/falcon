using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.UI;
using ImGuiNET;

namespace Falcon.Editor.Views
{
    public class PropertiesView : View
    {
        public string GroupName { get; set; }

        public List<EditorProperty> Properties { get; set; } = new List<EditorProperty>();

        public override void Render()
        {
            ImGui.Text(GroupName);
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
