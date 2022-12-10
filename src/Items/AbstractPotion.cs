using Merlin2d.Game.Actors;
using Merlin2d.Game;


namespace MyGame.Actors.Items
{
    public abstract class AbstractPotion : AbstractItem
    {
        protected bool used;
        protected int dose;
        protected Animation fullAnimation;
        protected Animation emptyAnimation;

        public AbstractPotion(int x, int y, int dose)
        {
            this.used = false;
            this.dose = dose;

            if (x >= 0 && y >= 0)
                SetPosition(x, y);
        }

        public override void Update() {

        }

        public override void Use(IActor actor)
        {
            used = true;

            SetAnimation(emptyAnimation);
            GetAnimation().Start();
        }
    }
}