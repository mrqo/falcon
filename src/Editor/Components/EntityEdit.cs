using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.UI;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components
{
    public class EntityEdit : Component
    {
        public List<GameComponent> ComponentViews { get; private set; } = new List<GameComponent>();

        public void Init(Entity entity)
        {
            ComponentViews = entity.Components
                .Select(GameComponent.Create)
                .ToList();
        }

        public override void Render()
        {
            foreach (var comp in ComponentViews)
            {
                comp.Render();
            }
        }
    }
}
