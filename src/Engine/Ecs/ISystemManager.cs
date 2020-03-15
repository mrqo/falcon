using System;
using System.Collections.Generic;

namespace Falcon.Engine.Ecs
{
    public interface ISystemManager
    {
        public IEnumerable<ISystem> Systems { get; }
        
        bool Disable<TSystem>() where TSystem : ISystem;

        bool Enable<TSystem>() where TSystem : ISystem;
        
        void Execute(float dt);
    }
}