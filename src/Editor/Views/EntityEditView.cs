using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.UI;

namespace Falcon.Editor.Views
{
    public class EntityEditView : View
    {
        public List<ComponentView> ComponentViews { get; private set; } = new List<ComponentView>();

        public void Init(Entity entity)
        {
            ComponentViews = entity.Components
                .Select(ComponentView.Create)
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
