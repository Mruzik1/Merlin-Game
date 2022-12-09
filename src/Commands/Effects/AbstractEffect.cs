using MyGame.Actors;


namespace MyGame.Commands
{
    public abstract class AbstractEffect: ICommand
    {
        protected ICharacter target;
        protected string name;

        public AbstractEffect(string name)
        {
            this.target = null;
            this.name = name;
        }

        public void SetTarget(ICharacter target)
        {
            this.target = target;
            target.AddEffect(this);
        }

        public void RemoveTarget()
        {
            if (target == null)
                return;
            
            target.RemoveEffect(this);
            target = null;
        }

        public virtual void Execute() {}
    }
}