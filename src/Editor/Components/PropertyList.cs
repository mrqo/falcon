using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.UI;
using ImGuiNET;

namespace Falcon.Editor.Components
{
    public class PropertyList : Component
    {
        public List<EditorProperty> Props { get; set; } = new List<EditorProperty>();

        public override void Render()
        {
            for (int i = 0; i < Props.Count; i++)
            {
                if (Props[i].PropertyType == typeof(double))
                {
                    double a = (double)Props[i].Value;
                    ImGui.InputDouble(Props[i].Name, ref a, 0.01);
                }

                if (Props[i].PropertyType == typeof(int))
                {
                    int a = (int) Props[i].Value;
                    ImGui.InputInt(Props[i].Name, ref a, 1);
                }
            }
        }
    }
}
