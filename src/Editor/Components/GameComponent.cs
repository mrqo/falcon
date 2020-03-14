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

namespace Falcon.Editor.Components
{
    public class GameComponent : Component
    {
        private Properties _propsComponent = new Properties();

        private Engine.Ecs.Component gameComponent;

        public static GameComponent Create(Engine.Ecs.Component component)
        {
            var view = new GameComponent();
            view.Init(component);
            return view;
        }

        public void Init(Engine.Ecs.Component component)
        {
            this.gameComponent = component;
            InitPropsView();
        }

        private void InitPropsView()
        {
            if (gameComponent == null)
            {
                return;
            }
            
            _propsComponent.Props = gameComponent
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute(typeof(CoProperty)) != null)
                .Select(prop => new EditorProperty
                {
                    Name = prop.Name,
                    Value = prop.GetValue(gameComponent),
                    PropertyType = prop.PropertyType
                })
                .ToList();
        }

        public override void Render() =>
            _propsComponent.Render();
    }
}
