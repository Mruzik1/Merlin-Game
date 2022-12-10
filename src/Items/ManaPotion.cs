using Merlin2d.Game.Actors;
using Merlin2d.Game;
using MyGame.Spells;


namespace MyGame.Actors.Items
{
    public class ManaPotion : AbstractPotion
    {
        public ManaPotion(int x, int y, int dose) : base(x, y, dose)
        {
            fullAnimation = new Animation("resources/sprites/mana_potion_full.png", 16, 16);
            emptyAnimation = new Animation("resources/sprites/potion_empty.png", 16, 16);

            SetAnimation(fullAnimation);
            GetAnimation().Start();
        }

        public ManaPotion(int dose) : this(-1, -1, dose) {}

        public override void Use(IActor actor)
        {
            if (used)
                return;

            if (actor is IWizard)
            {
                (actor as IWizard).ChangeMana(dose);
                base.Use(actor);
            }
        }
    }
}