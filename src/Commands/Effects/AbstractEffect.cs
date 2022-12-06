using MyGame.Actors;


namespace MyGame.Commands
{
    public abstract class AbstractEffect: ICommand
    {
        protected ICharacter target;

        public AbstractEffect()
        {
            this.target = null;
        }

        public void SetTarget(ICharacter target)
        {
            this.target = target;
            target.AddEffect(this);
        }

        public void RemoveTarget()
        {
            target.RemoveEffect(this);
            target = null;
        }

        public virtual void Execute() {}
    }
}