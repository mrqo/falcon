using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Editor.Views;
using Engine.EntityComponentModel;

namespace Editor.Controllers
{
    public class EntitiesController
    {
        protected EntitiesView _entitiesView;

        protected IEntityProvider _entityProvider;

        protected EntityEditorController _entityEditorController;

        public EntitiesController(EntitiesView view, IEntityProvider entityProvider)
        {
            _entitiesView = view;
            _entitiesView.OnCreateEventHandler = OnEntityCreatePressed;
            _entitiesView.Entities = new[]
            {
                "Player",
                "Car",
                "Something other"
            };

            _entityProvider = entityProvider;
            _entityEditorController = new EntityEditorController(view.EntityEditorView);
            _entityEditorController.CreateComponentEditorControllerHandler =
                view => new ComponentEditorController(view);

            _entityEditorController.Init(entityProvider.Entities.FirstOrDefault());
        }

        protected void OnEntityCreatePressed()
        {
            var entityOptionId = _entitiesView.SelectedEntityId;
            Console.WriteLine($"Create entity {entityOptionId}");
            Console.WriteLine($"Game has {_entityProvider.Entities.Count()} entities.");
        }
    }
}
