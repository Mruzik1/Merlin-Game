using Merlin2d.Game;
using Merlin2d.Game.Enums;
using Merlin2d.Game.Actors;
using MyGame.Actors.Items;


namespace MyGame.Actors
{
    public class EndCrystal : AbstractActor, IUsable
    {
        private Animation animationDefault;

        public EndCrystal(int x, int y)
        {
            animationDefault = new Animation("resources/sprites/crystal_on.png", 56, 56);
            
            SetAnimation(animationDefault);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void Use(IActor actor)
        {   
            if (actor is Player && (actor as Player).GetInventory().GetItem() is EndKey)
                GetWorld().SetEndCondition(w => MapStatus.Finished);
        }

        public override void Update() {}
    }
}