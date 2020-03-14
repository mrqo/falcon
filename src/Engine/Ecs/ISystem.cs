namespace Falcon.Engine.Ecs
{
    public interface ISystem
    {
        double Dt { get; set; }

        void Step();
    }
}