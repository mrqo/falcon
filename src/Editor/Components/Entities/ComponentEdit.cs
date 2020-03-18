using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Falcon.Engine.Ecs;
using Falcon.Engine.UI;
using ImGuiNET;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components.Entities
{
    public class ComponentEdit : Component
    {
        private Engine.Ecs.Component _gameComponent;

        private int _componentId = -1;

        public int ComponentId
        {
            get => _componentId;
            set
            {
                _componentId = value;
                OnComponentIdSet();
            }
        }

        private readonly IComponentProvider _componentProvider;
        
        public ComponentEdit(IComponentProvider componentProvider)
        {
            _componentProvider = componentProvider;
        }
        
        private void OnComponentIdSet()
        {
            _gameComponent = _componentProvider.Find(_componentId);
        }

        public override void Render()
        {
            _gameComponent
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute(typeof(CoProperty)) != null)
                .ToList()
                .ForEach(prop =>
                {
                    var value = prop.GetValue(_gameComponent);
                    if (prop.PropertyType == typeof(double))
                    {
                        double a = (double)value;
                        ImGui.InputDouble(prop.Name, ref a, 0.01);
                        value = a;
                    }

                    if (prop.PropertyType == typeof(int))
                    {
                        int a = (int) value;
                        ImGui.InputInt(prop.Name, ref a, 1);
                        value = a;
                    }

                    prop.SetValue(_gameComponent, value);
                });
        }
    }
}
