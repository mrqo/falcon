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

        protected ComponentEditorController _compEditController;

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
            _compEditController = new ComponentEditorController(view.CompEditView);
            _compEditController.Init(entityProvider.Entities.FirstOrDefault().Components.FirstOrDefault());
        }

        protected void OnEntityCreatePressed()
        {
            var entityOptionId = _entitiesView.SelectedEntityId;
            Console.WriteLine($"Create entity {entityOptionId}");
            Console.WriteLine($"Game has {_entityProvider.Entities.Count()} entities.");
        }
    }
}
