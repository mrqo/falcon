using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.World;
using Ninject;

namespace Falcon.Engine.Implementation.EntityComponentModel
{
    public class EntityFactory : IEntityFactory
    {
        private IKernel _kernel;

        public EntityFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public TEntity Create<TEntity>() where TEntity : Entity
        {
            return _kernel.Get<TEntity>();
        }
    }
}
