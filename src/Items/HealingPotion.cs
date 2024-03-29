using Merlin2d.Game.Actors;
using Merlin2d.Game;


namespace MyGame.Actors.Items
{
    public class HealingPotion : AbstractPotion
    {
        public HealingPotion(int x, int y, int dose) : base(x, y, dose)
        {
            fullAnimation = new Animation("resources/sprites/healing_potion_full.png", 16, 16);
            emptyAnimation = new Animation("resources/sprites/potion_empty.png", 16, 16);

            SetAnimation(fullAnimation);
            GetAnimation().Start();
        }

        public HealingPotion(int dose) : this(-1, -1, dose) {}

        public override void Use(IActor actor)
        {
            if (used)
                return;

            if (actor is ICharacter)
            {
                (actor as ICharacter).ChangeHealth(dose);
                base.Use(actor);
            }
        }
    }
}