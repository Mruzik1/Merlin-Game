using Merlin2d.Game.Items;
using Merlin2d.Game.Actors;
using Merlin2d.Game;


namespace MyGame.Actors.Items
{
    public class HealingPotion : AbstractActor, IItem, IUsable
    {
        private bool used;
        private int dose;
        private Animation fullAnimation;
        private Animation emptyAnimation;

        public HealingPotion(int x, int y, int dose)
        {
            used = false;
            this.dose = dose;

            fullAnimation = new Animation("resources/sprites/source_on.png", 16, 16);
            emptyAnimation = new Animation("resources/sprites/source_off.png", 16, 16);

            SetPosition(x, y);
            SetAnimation(fullAnimation);
            GetAnimation().Start();
        }

        public override void Update()
        {
            
        }

        public void Use(IActor actor)
        {
            if (used)
                return;

            if (actor is ICharacter)
            {
                ICharacter character = (ICharacter)actor;
                character.ChangeHealth(dose);
                used = true;

                SetAnimation(emptyAnimation);
                GetAnimation().Start();
            }
        }
    }
}