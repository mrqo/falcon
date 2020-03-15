using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.Ecs;
using Falcon.Engine.UI;
using ImGuiNET;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components.Entities
{
    public class ComponentEdit : Component
    {
        private PropertyList _propertyList = new PropertyList();

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
        
        private void OnComponentIdSet()
        {
            _gameComponent = null; // #TODO: Create ComponentProvider and get by id
            if (_gameComponent == null)
            {
                return;
            }
            
            _propertyList.Props = _gameComponent
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute(typeof(CoProperty)) != null)
                .Select(prop => new EditorProperty
                {
                    Name = prop.Name,
                    Value = prop.GetValue(_gameComponent),
                    PropertyType = prop.PropertyType
                })
                .ToList();
        }
        
        public override void Render() =>
            _propertyList.Render();
    }
}
