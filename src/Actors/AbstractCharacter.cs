using MyGame.Commands;
using MyGame.Spells;
using Merlin2d.Game;


namespace MyGame.Actors
{
    public abstract class AbstractCharacter : AbstractActor, ICharacter
    {
        private ISpeedStrategy speedStrategy;
        private ICharacterState state;
        protected int health;
        protected int maxHealth;
        protected double speed;
        private int invincibilityTime;
        private int invincibility;
        protected Message displayHealth;
        private List<ICommand> effects;
        private string[] damagers;
        private Jump jump;
        protected ICommand moveLeft;
        protected ICommand moveRight;
        private ActorOrientation direction;

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

        public bool IsInvincible() => invincibility > 0;

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
            if (!(this is IWizard))
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
            // negative health = immortality
            if (health < 0)
                return;
            
            if (health+delta > 0 && health+delta <= maxHealth)
                health += delta;
            else if (health+delta <= 0)
            {
                Die();
                return;
            }
            else if (health+delta > maxHealth)
                health = maxHealth;

            if (!(this is IWizard))
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
            displayHealth.SetText($"DEAD {GetName()}");
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

            // Taking Damage
            if (invincibility > 0)
                invincibility--;
            else
                TakeDamage();
        }

        public abstract void Walking();
    }
}