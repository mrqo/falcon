using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.UI
{
    public abstract class Component
    {
        public virtual void Init()
        {
        }
        
        public abstract void Render();

        public virtual void Dispose()
        {
        }
    }
}
