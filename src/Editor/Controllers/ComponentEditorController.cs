using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Editor.PartialViews;
using Falcon.Engine.EntityComponentModel;

namespace Falcon.Editor.Controllers
{
    public class ComponentEditorController
    {
        protected ComponentEditorView _editorView;

        public ComponentEditorController(ComponentEditorView view)
        {
            _editorView = view;
        }

        public void Init(Component component)
        {
            _editorView.ComponentName = component.GetType().Name;
            _editorView.Properties = component
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
    }
}
