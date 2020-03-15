using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class SystemManager : ISystemManager
    {
        private readonly List<ISystem> _systems;
        
        private readonly Dictionary<string, ISystem> _typeNamesToSystems;
        
        public IEnumerable<ISystem> Systems => _systems;

        public SystemManager(ISystem[] systems)
        {
            _systems = systems.ToList();
            _typeNamesToSystems = _systems
                .ToDictionary(sys => sys.GetType().FullName, sys => sys);
        }
        
        public bool Disable<TSystem>() where TSystem : ISystem =>
            SetActiveFlag<TSystem>(false);

        public bool Enable<TSystem>() where TSystem : ISystem =>
            SetActiveFlag<TSystem>(true);
        
        private bool SetActiveFlag<TSystem>(bool isActive)
        {
            var fullName = typeof(TSystem).FullName;
            if (fullName == null)
            {
                return false;
            }
            
            var system = _typeNamesToSystems[fullName];
            if (system == null)
            {
                return false;
            }

            system.IsActive = isActive;
            return true;
        }

        public void Execute(float dt) =>
            _systems
                .Where(system => system.IsActive)
                .ToList()
                .ForEach(system =>
                {
                    system.Dt = dt;
                    system.Step();
                });
    }
}