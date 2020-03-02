using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.UI;

namespace Falcon.Editor.PartialViews
{
    public class EntityEditorView : View
    {
        public List<ComponentEditorView> ComponentEditorViews { get; set; } = new List<ComponentEditorView>();

        public override void Render()
        {
            foreach (var comp in ComponentEditorViews)
            {
                comp.Render();
            }
        }
    }
}
