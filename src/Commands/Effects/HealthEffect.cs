using MyGame.Actors;


namespace MyGame.Commands
{
    public class HealthEffect: AbstractEffect
    {
        protected int hp;

        public HealthEffect(string name) : base(name)
        {
            
        }

        public void SetHp(int hp)
        {
            this.hp = hp;
        }

        public override void Execute()
        {
            if (target != null)
                target.ChangeHealth(hp);
            RemoveTarget();
        }
    }
}