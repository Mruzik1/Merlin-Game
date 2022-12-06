using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MyGame.Actors;


namespace MyGame.Factories
{
    public class ActorFactory : IFactory
    {   
        public IActor Create(string actorType, string name, int x, int y)
        {
            IActor actor = null;

            if (actorType == "Player")
            {
                actor = new Player(x, y, 2, 100, 100);
                actor.SetName(name);
                actor.SetPhysics(true);
            }

            else if (actorType == "Enemy")
            {
                actor = new Skeleton(x, y, 1.5, 200, 200);
                actor.SetName(name);
                actor.SetPhysics(true);
            }

            // temporary actor to test IntersectsWithActor()
            else if (actorType == "TestActor")
            {
                actor = new TestActor(x, y);
                actor.SetName(name);
            }
            
            return actor;
        }
    }
}