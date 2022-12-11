using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MyGame.Actors.Items;
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
                actor = new Player(x, y, 2, 100, 0);
                string[] enemies = {"John"};

                actor.SetPhysics(true);
                (actor as AbstractCharacter).SetInvincibility(1000);
                (actor as AbstractCharacter).SetDamagers(enemies);
            }

            else if (actorType == "Enemy")
            {
                actor = new Skeleton(x, y, 1.5, 150, 250);
                actor.SetPhysics(true);
            }

            else if (actorType == "BoxObj")
            {
                actor = new Box(x, y);
                actor.SetPhysics(true);
            }

            else if (actorType.Contains("DoorObj"))
                actor = new Door(x, y, actorType);

            else if (actorType == "PressurePlateObj")
                actor = new PressurePlate(x, y);

            else if (actorType == "SwitchObj")
                actor = new Lever(x, y);

            else if (actorType == "EndCrystalObj")
                actor = new EndCrystal(x, y);

            else if (actorType == "HealingPotionObj")
                actor = new HealingPotion(x, y, 30);

            else if (actorType == "ManaPotionObj")
                actor = new ManaPotion(x, y, 120);
            
            else if (actorType == "EndKeyObj")
                actor = new EndKey(x, y);

            if (actor != null)
                actor.SetName(name);
            
            return actor;
        }
    }
}