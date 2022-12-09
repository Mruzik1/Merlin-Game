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
                actor.SetPhysics(true);
            }

            else if (actorType == "Enemy")
            {
                actor = new Skeleton(x, y, 1.5, 200, 300);
                actor.SetPhysics(true);
            }

            else if (actorType == "BoxObj")
            {
                actor = new Box(x, y);
                actor.SetPhysics(true);
            }

            else if (actorType == "DoorObj")
            {
                actor = new Door(x, y);
            }

            else if (actorType == "PressurePlateObj")
            {
                actor = new PressurePlate(x, y);
            }
            else if (actorType == "SwitchObj")
            {
                actor = new Lever(x, y);
            }

            if (actor != null)
                actor.SetName(name);
            
            return actor;
        }
    }
}