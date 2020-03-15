using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcon.Editor.Models;
using Falcon.Engine.Ecs;
using Falcon.Engine.UI;
using ImGuiNET;
using Ninject;
using Component = Falcon.Engine.UI.Component;

namespace Falcon.Editor.Components
{
    public class ComponentsEdit : Component
    {
        private List<KeyValuePair<string, ComponentEdit>> _components
            = new List<KeyValuePair<string, ComponentEdit>>();

        private readonly IEntityProvider _entityProvider;
        
        private int _entityId = -1;

        private Entity _entity;
        
        public int EntityId
        {
            get => _entityId;
            set
            {
                _entityId = value;
                OnEntityIdSet();
            }
        }

        private IKernel _kernel;
        
        public ComponentsEdit(
            IEntityProvider entityProvider,
            IKernel kernel)
        {
            _entityProvider = entityProvider;
            _kernel = kernel;
        }

        private void OnEntityIdSet()
        {
            _entity = _entityProvider.Get(EntityId);
            _components.Clear();

            if (_entity == null)
            {
                return;
            }
            
            _components = _entity.Components
                .Select(comp =>
                {
                    var compEdit = _kernel.Get<ComponentEdit>();
                    var compName = comp.GetType().FullName;
                    compEdit.ComponentId = comp.GetHashCode();
                    
                    return new KeyValuePair<string, ComponentEdit>(compName, compEdit);
                })
                .ToList();
        }
        
        public override void Render() =>
            _components.ForEach(comp =>
            {
                if (ImGui.TreeNode(comp.Key))
                {
                    comp.Value.Render();
                    ImGui.TreePop();
                }
            });
    }
}
