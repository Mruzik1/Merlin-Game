using MyGame.Commands;
using Merlin2d.Game;


namespace MyGame.Actors
{
    public abstract class AbstractCharacter : AbstractActor, ICharacter
    {
        protected ISpeedStrategy speedStrategy;
        protected ICharacterState state;
        protected int health;
        protected int maxHealth;
        protected double speed;
        protected Message displayHealth;
        protected List<ICommand> effects;
        protected Jump jump;
        protected ICommand moveLeft;
        protected ICommand moveRight;
        protected ActorOrientation direction;

        public AbstractCharacter(double speed, int health)
        {
            this.speed = speed;
            this.health = health;
            maxHealth = health;

            effects = new List<ICommand>();

            moveRight = new Move(this, 1, 0);
            moveLeft = new Move(this, -1, 0);
            
            direction = ActorOrientation.FacingRight;

            speedStrategy = new NormalSpeedStrategy();
            state = new LivingState(this);
        }

        public int GetHealth() => health;

        public void InitHealthMsg()
        {
            displayHealth = new Message($"{health} / {maxHealth} HP", -20, -20, 15, Color.White, Merlin2d.Game.Enums.MessageDuration.Indefinite);
            displayHealth.SetAnchorPoint(this);
            GetWorld().AddMessage(displayHealth);
        }

        public void ChangeHealth(int delta)
        {
            if (health > 0)
                health += delta;

            if (health < 0)
                health = 0;

            displayHealth.SetText($"{health} / {maxHealth} HP");
        }

        public void SetJump(Jump jump)
        {
            this.jump = jump;
        }

        public void Die()
        {
            state = new DyingState(this);
            displayHealth.SetText("DEAD");
        }

        public void AddEffect(ICommand effect)
        {
            effects.Add(effect);
        }

        public void RemoveEffect(ICommand effect)
        {
            effects.Remove(effect);
        }

        public List<ICommand> GetEffects()
        {
            return effects;
        }

        public void SetSpeedStrategy(ISpeedStrategy strategy)
        {
            speedStrategy = strategy;
        }

        public double GetSpeed() => speedStrategy.GetSpeed(speed);

        public void ChangeDirection(ActorOrientation newDirection)
        {
            if (direction != newDirection)
            {
                direction = newDirection;
                animation.FlipAnimation();
            }
        }

        public void MakeJump(bool condition)
        {
            if (condition)
            {   
                SetPosition(GetX(), GetY()+3);
                
                if (!jump.IsJumping() && GetWorld().IntersectWithWall(this))
                    jump.performJump(GetY());

                SetPosition(GetX(), GetY()-3);
            }
            if (jump.IsJumping())
                jump.Execute();
        }

        public override void Update()
        {
            state.Update();

            // dying
            if (health <= 0)
                Die();
        }

        public abstract void Walking();
    }
}