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
                actor = new Player(x, y, 2, 100, 2000);
                string[] enemies = {"John"};

                actor.SetPhysics(true);
                (actor as AbstractCharacter).SetInvincibility(1000);
                (actor as AbstractCharacter).SetDamagers(enemies);
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
                (actor as AbstractCharacter).SetInvincibility(100);
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

            else if (actorType == "EndCrystalObj")
            {
                actor = new EndCrystal(x, y);
            }

            if (actor != null)
                actor.SetName(name);
            
            return actor;
        }
    }
}