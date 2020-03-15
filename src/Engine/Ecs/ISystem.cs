namespace Falcon.Engine.Ecs
{
    public interface ISystem
    {
        bool IsActive { get; set; }
        
        double Dt { get; set; }

        void Step();
    }
}