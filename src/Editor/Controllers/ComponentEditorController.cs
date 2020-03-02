using System;
using System.Collections.Generic;
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
            _editorView.Properties.Clear();

            var compType = component.GetType();
            var properties = compType.GetProperties();

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute(typeof(CoProperty)) != null)
                {
                    _editorView.Properties.Add(new EditorProperty
                    {
                        Name = property.Name,
                        Value = property.GetValue(component),
                        PropertyType = property.PropertyType
                    });
                }
            }
        }
    }
}
