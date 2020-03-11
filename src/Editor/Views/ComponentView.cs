using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.UI;

namespace Falcon.Editor.Views
{
    public class ComponentView : View
    {
        private PropertiesView propsView = new PropertiesView();

        private Component component;

        public static ComponentView Create(Component component)
        {
            var view = new ComponentView();
            view.Init(component);
            return view;
        }

        public void Init(Component component)
        {
            this.component = component;
            InitPropsView();
        }

        private void InitPropsView()
        {
            if (component == null)
            {
                return;
            }

            propsView.GroupName = component.GetType().Name;
            propsView.Properties = component
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute(typeof(CoProperty)) != null)
                .Select(prop => new EditorProperty
                {
                    Name = prop.Name,
                    Value = prop.GetValue(component),
                    PropertyType = prop.PropertyType
                })
                .ToList();
        }

        public override void Render()
        {
            propsView.Render();
        }
    }
}
