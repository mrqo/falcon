﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Engine.EntityComponentModel;

namespace Engine.Implementation.EntityComponentModel
{
    public class ComponentResolver : IComponentResolver
    {
        private List<Component> _components;

        public IReadOnlyCollection<Component> Components => _components;

        public ComponentResolver()
        {
            _components = new List<Component>();
        }

        public virtual void AddComponent(Component component)
        {
            _components.Add(component);
        }

        public virtual TComponent FindComponent<TComponent>() where TComponent : Component
        {
            foreach (var comp in _components)
            {
                
                if (comp.GetType() == typeof(TComponent))
                {
                    return comp as TComponent;
                }
            }

            return null;
        }
    }
}