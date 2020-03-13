using Falcon.Engine.Communication;

namespace Falcon.Engine.Ecs
{
    public class EntityDeps
    {
        public INotificationHub NotificationHub { get; }
        
        public IComponentResolver ComponentResolver { get; }

        public EntityDeps(
            INotificationHub notificationHub,
            IComponentResolver componentResolver)
        {
            NotificationHub = notificationHub;
            ComponentResolver = componentResolver;
        }
    }
}