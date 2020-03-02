using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Editor.PartialViews;
using Falcon.Engine.EntityComponentModel;

namespace Falcon.Editor.Controllers
{
    public class EntityEditorController
    {
        protected EntityEditorView _editorView;

        public delegate ComponentEditorController CreateComponentEditorController(ComponentEditorView view);

        public CreateComponentEditorController CreateComponentEditorControllerHandler { get; set; }

        public EntityEditorController(EntityEditorView view)
        {
            _editorView = view;
        }

        public void Init(Entity entity)
        {
            var editorEntity = new EditorEntity();
            editorEntity.Name = entity.GetType().Name;

            foreach (var component in entity.Components)
            {
                var componentView = new ComponentEditorView();
                var componentController = CreateComponentEditorControllerHandler(componentView);
                componentController.Init(component);
                _editorView.ComponentEditorViews.Add(componentView);
            }
        }
    }
}
