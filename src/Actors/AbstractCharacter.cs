using MyGame.Commands;
using MyGame.Spells;
using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public abstract class AbstractCharacter : AbstractActor, ICharacter
    {
        protected ISpeedStrategy speedStrategy;
        protected ICharacterState state;
        protected int health;
        protected int maxHealth;
        protected double speed;
        protected int invincibilityTime;
        protected int invincibility;
        protected Message displayHealth;
        protected List<ICommand> effects;
        protected string[] damagers;
        protected Jump jump;
        protected ICommand moveLeft;
        protected ICommand moveRight;
        protected ActorOrientation direction;

        public AbstractCharacter(double speed, int health)
        {
            this.speed = speed;
            this.health = health;
            maxHealth = this.health;

            invincibility = 0;
            damagers = Array.Empty<string>();

            effects = new List<ICommand>();

            moveRight = new Move(this, 1, 0);
            moveLeft = new Move(this, -1, 0);
            
            direction = ActorOrientation.FacingRight;

            speedStrategy = new NormalSpeedStrategy();
            state = new LivingState(this);
        }

        public int GetHealth() => health;

        public void SetDamagers(string[] damagers)
        {
            this.damagers = damagers;
        }

        public void SetInvincibility(int invincibilityTime)
        {
            this.invincibilityTime = (invincibilityTime / 1000) * 60;
        }

        public void InitHealthMsg()
        {
            if ((this as IWizard) == null)
                displayHealth = new Message($"{health} / {maxHealth} HP",
                -20, -20, 15, Color.Black, Merlin2d.Game.Enums.MessageDuration.Indefinite);
            else
                displayHealth = new Message($"{health} / {maxHealth} HP\n{(this as IWizard).GetMana()} MP",
                -20, -40, 15, Color.Black, Merlin2d.Game.Enums.MessageDuration.Indefinite);

            displayHealth.SetAnchorPoint(this);
            GetWorld().AddMessage(displayHealth);
        }

        public void ChangeHealth(int delta)
        {
            if (health > 0)
                health += delta;

            if (health < 0)
                health = 0;

            if ((this as IWizard) == null)
                displayHealth.SetText($"{health} / {maxHealth} HP");
            else
                displayHealth.SetText($"{health} / {maxHealth} HP\n{(this as IWizard).GetMana()} MP");
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

        public ActorOrientation GetDirection() => direction;

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

        public void TakeDamage()
        {
            foreach (string damagerName in damagers)
            {
                GetWorld().GetActors().ForEach(actor => {
                    if (actor.GetName().Contains(damagerName) && IntersectsWithActor(actor))
                    {
                        invincibility = invincibilityTime;
                        ChangeHealth(-20);
                    }
                });
            }
        }

        public override void Update()
        {
            state.Update();

            // dying
            if (health <= 0)
                Die();

            // Taking Damage
            if (invincibility > 0)
                invincibility--;
            else
                TakeDamage();
        }

        public abstract void Walking();
    }
}